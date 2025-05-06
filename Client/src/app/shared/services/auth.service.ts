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
  isAuthenticated():
    boolean {
    return !!this.tokenService.accessToken;
  }
}
