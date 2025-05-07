import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, tap} from 'rxjs';
import {ApiResult} from '../model/api.interface';
import {environment} from '../../../environments/environment';
import {Token} from '../model/token.interface';
import {TokenService} from './token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  http = inject(HttpClient)
  tokenService = inject(TokenService)
  apiUrl = environment.baseUrl + 'auth';

  login(form: any): Observable<ApiResult<Token>> {
    return this.http.post<ApiResult<Token>>(this.apiUrl + '/login', form).pipe(
      tap((res) => {
        if (res.success) {
          console.log(res.data);
          this.tokenService.saveToken(res.data);
        }
      })
    );
  }

  register(form: any): Observable<ApiResult<any>> {
    return this.http.post<ApiResult<any>>(this.apiUrl + '/register', form)
  }

  logout(): void {
    const refreshToken = this.tokenService.refreshToken;
    if (refreshToken) {
      this.http.post(this.apiUrl + `/logout?refreshToken=${refreshToken}`, {}).subscribe()
    }
    this.tokenService.clearToken();
  }

  requestPasswordReset(form: any): Observable<ApiResult<any>> {
    return this.http.post<ApiResult<any>>(this.apiUrl + '/forgot-password', form)
  }

  confirmPasswordReset(form: any): Observable<ApiResult<any>> {
    return this.http.post<ApiResult<any>>(this.apiUrl + '/reset-password', form)
  }

  changePassword(form: any): Observable<ApiResult<any>> {
    return this.http.post<ApiResult<any>>(this.apiUrl + '/change-password', form)
  }

  isAuthenticated(): boolean {
    return !!this.tokenService.accessToken;
  }
}
