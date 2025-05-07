import {inject, Injectable, PLATFORM_ID} from '@angular/core';
import {isPlatformBrowser} from '@angular/common';
import {ACCESS_TOKEN, CLIENT_ID, REFRESH_TOKEN} from '../constant';
import {CookieService} from 'ngx-cookie-service';
import {JwtPayload, Token} from '../model/token.interface';
import {environment} from '../../../environments/environment';
import {jwtDecode} from 'jwt-decode';
import {BehaviorSubject, catchError, Observable, of, switchMap, tap, throwError, timer} from 'rxjs';
import {ApiResult} from '../model/api.interface';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  platFormId = inject(PLATFORM_ID)
  isBrowser = isPlatformBrowser(this.platFormId);
  http = inject(HttpClient)

  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

  cookieService = inject(CookieService)
  private userId: string | null = null;

  private retryCount = 0;
  private maxRetries = 3;

  constructor() {
    this.initUserFromToken();
  }

  private initUserFromToken(): void {
    const token = this.accessToken;
    if (token) {
      const payload = this.getUserInfoFromToken(token);
      if (payload?.sub) {
        this.userId = payload.sub;
      }
    }
  }

  get accessToken(): string | null {
    if (this.isBrowser)
      return this.cookieService.get(ACCESS_TOKEN) || '';
    return null;
  }

  get refreshToken(): string | null {
    if (this.isBrowser)
      return this.cookieService.get(REFRESH_TOKEN) || '';
    return null;
  }

  get clientId(): string | null {
    if (this.isBrowser)
      return this.cookieService.get(CLIENT_ID) || '';
    return null;
  }

  saveToken(token: Token): void {
    if (this.isBrowser) {
      const refreshExpires = new Date(token.expiresAt);
      this.cookieService.set(CLIENT_ID, token.clientId, {
        expires: refreshExpires,
        path: '/',
        sameSite: "None",
        domain: environment.domain,
        secure: environment.production
      })
      this.cookieService.set(ACCESS_TOKEN, token.accessToken, {
        expires: refreshExpires,
        path: '/',
        sameSite: "None",
        domain: environment.domain,
        secure: environment.production
      });
      this.cookieService.set(REFRESH_TOKEN, token.refreshToken, {
        expires: refreshExpires,
        path: '/',
        sameSite: "None",
        domain: environment.domain,
        secure: environment.production
      });

      const payload = this.getUserInfoFromToken(token.accessToken);
      if (payload?.sub) {
        this.userId = payload.sub;
      }
    }
  }

  getUserInfoFromToken(token: string): JwtPayload | null {
    try {
      return jwtDecode<JwtPayload>(token);
    } catch (error) {
      if (!environment.production) {
        console.error('Invalid token', error);
      }
      return null;
    }
  }

  clearToken(): void {
    if (this.isBrowser) {
      this.cookieService.delete(ACCESS_TOKEN, '/', environment.domain, environment.production, 'None');
      this.cookieService.delete(REFRESH_TOKEN, '/', environment.domain, environment.production, 'None');
      this.cookieService.delete(CLIENT_ID, '/', environment.domain, environment.production, 'None');

      this.userId = null;
    }
  }

  refreshTokenHandle(isRetry = false): Observable<ApiResult<Token>> {
    if (!isRetry) {
      this.retryCount = 0;
    }

    if (!this.isBrowser) {
      return throwError(() => new Error('Not running in a browser environment'));
    }

    const token = this.refreshToken;
    const clientId = this.clientId;

    if (!token) {
      this.clearToken();
      return throwError(() => new Error('No refresh token available'));
    }

    if (!clientId) {
      this.clearToken();
      return throwError(() => new Error('No client ID available'));
    }

    if (!this.userId) {
      this.clearToken();
      return throwError(() => new Error('No user ID available'));
    }

    const baseUrl = environment.baseUrl;

    const tokenRequest: TokenRequest = {
      refreshToken: token,
      clientId: clientId,
      userId: this.userId
    };

    return this.http.post<ApiResult<Token>>(`${baseUrl}auth/refresh-token`, tokenRequest).pipe(
      tap((response) => {
        if (response.success && response.data) {
          this.saveToken(response.data);
          // Reset counter sau khi thành công
          this.retryCount = 0;
        }
      }),
      catchError((error) => {
        this.retryCount++;
        if (this.retryCount <= this.maxRetries) {
          if (!environment.production) {
            console.log(`Retry attempt ${this.retryCount} of ${this.maxRetries}`);
          }
          return timer(1500).pipe(
            switchMap(() => this.refreshTokenHandle(true))
          );
        }
        if (!environment.production) {
          console.log(`Maximum retries (${this.maxRetries}) reached`);
        }
        this.clearToken();
        this.retryCount = 0;
        window.location.reload()
        return throwError(() => error);
      })
    );
  }

  handleUnauthorized(): Observable<boolean> {
    if (this.isRefreshing) {
      return this.refreshTokenSubject.pipe(
        switchMap((result) => {
          if (result) {
            return of(true);
          } else {
            return of(false);
          }
        })
      );
    }

    this.isRefreshing = true;
    this.refreshTokenSubject.next(null);

    return this.refreshTokenHandle().pipe(
      switchMap((response) => {
        this.isRefreshing = false;
        this.refreshTokenSubject.next(response);
        return of(true);
      }),
      catchError((error) => {
        this.isRefreshing = false;
        this.refreshTokenSubject.next(false);
        return of(false);
      })
    );
  }

  getClaims() {
    const token = this.accessToken;
    if (!token) return null;

    const decoded: any = jwtDecode(token);
    return {
      userId: decoded.sub,
      username: decoded.unique_name,
      role: decoded.typ
    };
  }
}

export class TokenRequest {
  refreshToken: string;
  clientId: string;
  userId: string;

  constructor(refreshToken: string, clientId: string, userId: string) {
    this.refreshToken = refreshToken;
    this.clientId = clientId;
    this.userId = userId;
  }
}
