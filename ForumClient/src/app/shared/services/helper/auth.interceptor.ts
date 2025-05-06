import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpHandlerFn,
  HttpInterceptor,
  HttpInterceptorFn,
  HttpRequest
} from '@angular/common/http';
import {catchError, Observable, switchMap, throwError} from 'rxjs';
import {inject} from '@angular/core';
import {TokenService} from '../token.service';
import {Router} from '@angular/router';

export function authInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> {
  const tokenService = inject(TokenService);
  const router = inject(Router);
  if (req.url.includes('/auth/refresh-token')) {
    return next(req);
  }

  const accessToken = tokenService.accessToken;
  let authReq = req;

  if (accessToken) {
    authReq = addTokenHeader(req, accessToken);
  }
  return next(authReq).pipe(
    catchError((error) => {
      if (error instanceof HttpErrorResponse && error.status === 401) {
        return handle401Error(authReq, next, tokenService, router);
      }
      return throwError(() => error);
    })
  );
}

function addTokenHeader(request: HttpRequest<any>, token: string): HttpRequest<any> {
  return request.clone({
    headers: request.headers.set('Authorization', `Bearer ${token}`)
  });
}

function handle401Error(
  request: HttpRequest<any>,
  next: HttpHandlerFn,
  tokenService: TokenService,
  router: Router
): Observable<HttpEvent<any>> {
  return tokenService.handleUnauthorized().pipe(
    switchMap((success) => {
      if (success && tokenService.accessToken) {
        const authReq = addTokenHeader(request, tokenService.accessToken);
        return next(authReq);
      } else {
        return throwError(() => new Error('Unauthorized'));
      }
    })
  );
}
