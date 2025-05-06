import {inject, Injectable, PLATFORM_ID} from '@angular/core';
import {isPlatformBrowser} from '@angular/common';
import {ACCESS_TOKEN, REFRESH_TOKEN} from '../constant';
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

  constructor() {
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

  saveToken(token: Token): void {
    if (this.isBrowser) {
      const refreshExpires = new Date(token.expiresAt);
      this.cookieService.set(ACCESS_TOKEN, token.accessToken, {
        expires: refreshExpires,
        path: '/',
        sameSite: 'None',
        domain: environment.domain,
        secure: environment.production
      });
      this.cookieService.set(REFRESH_TOKEN, token.refreshToken, {
        expires: refreshExpires,
        path: '/',
        sameSite: 'None',
        domain: environment.domain,
        secure: environment.production
      });
    }
  }

  getUserInfoFromToken(token: string): JwtPayload | null {
    try {
      return jwtDecode<JwtPayload>(token);
    } catch (error) {
      console.error('Invalid token', error);
      return null;
    }
  }

  clearToken(): void {
    if (this.isBrowser) {
      this.cookieService.delete(ACCESS_TOKEN, '/');
      this.cookieService.delete(REFRESH_TOKEN, '/');
    }
  }

  refreshTokenHandle(): Observable<ApiResult<Token>> {
    if (!this.refreshToken) {
      this.clearToken();
      return throwError(() => new Error('No refresh token available'));
    }

    const token = this.refreshToken
    const baseUrl = environment.baseUrl;
    let retryCount = 0;
    const maxRetries = 3;

    return this.http.post<ApiResult<Token>>(`${baseUrl}auth/refresh-token?refreshToken=${token}`, {}).pipe(
      tap((response) => {
        if (response.success && response.data) {
          this.saveToken(response.data);
        }
      }),
      catchError((error) => {
        retryCount++;
        if (retryCount <= maxRetries) {
          return timer(1500).pipe(
            switchMap(() => this.refreshTokenHandle())
          );
        }
        this.clearToken();
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
