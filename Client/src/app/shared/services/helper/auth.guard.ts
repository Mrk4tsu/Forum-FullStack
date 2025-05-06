import {CanActivateFn, Router} from '@angular/router';
import {AuthService} from '../auth.service';
import {inject, PLATFORM_ID} from '@angular/core';
import {TokenService} from '../token.service';
import {isPlatformBrowser} from '@angular/common';
import {environment} from '../../../../environments/environment';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const tokenService = inject(TokenService);
  const router = inject(Router);


  if (authService.isAuthenticated()) {
    const claimReq = route.data['claimReq'] as Function;
    if (claimReq) {
      const claims = tokenService.getClaims();
      if (!claimReq(claims)) {
        router.navigateByUrl('/forbidden')
        return false
      }
      return true
    }
    return true;
  } else {
    goToLogin()
    return false
  }
}

export function goToLogin() {
  const isBrowser = isPlatformBrowser(inject(PLATFORM_ID))
  if (isBrowser)
    window.location.href = environment.baseSubdomain + '/login';
}
