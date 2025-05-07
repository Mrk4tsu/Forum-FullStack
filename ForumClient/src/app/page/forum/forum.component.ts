import {ChangeDetectorRef, Component, inject, OnInit, PLATFORM_ID} from '@angular/core';
import {CommonModule, isPlatformBrowser} from '@angular/common';
import {RouterLink, RouterOutlet} from '@angular/router';
import {AuthService} from '../../shared/services/auth.service';
import {TokenService} from '../../shared/services/token.service';
import {JwtPayload} from '../../shared/model/token.interface';
import {claimReq} from '../../shared/services/helper/claimReq-utils';
import {
  HideContentIfClaimsNotMetDirective
} from '../../shared/services/helper/hide-content-if-claims-not-met.directive';

@Component({
  selector: 'app-forum',
  imports: [CommonModule, RouterOutlet, HideContentIfClaimsNotMetDirective, RouterLink],
  templateUrl: './forum.component.html',
  styleUrl: './forum.component.css'
})
export class ForumComponent implements OnInit {
  authService = inject(AuthService)
  tokenService = inject(TokenService)
  isBrowser = isPlatformBrowser(inject(PLATFORM_ID))

  isAuthorize = false;
  userInfo: JwtPayload | null = null;

  ngOnInit() {
    this.isAuthorize = this.authService.isAuthenticated();
    if (this.isAuthorize) {
      const accessToken = this.tokenService.accessToken;
      if (accessToken) {
        this.userInfo = this.tokenService.getUserInfoFromToken(accessToken);
      }
    }
  }

  onLogout(): void {
    this.authService.logout();
    this.isAuthorize = false;
    window.location.reload();
  }

  refreshToken(): void {
    this.tokenService.refreshTokenHandle().subscribe({
      next: (res) => {
        if (res.success) {
          this.tokenService.saveToken(res.data);
          this.userInfo = this.tokenService.getUserInfoFromToken(res.data.accessToken);
        }
      }, error: (err) => {
        console.log(err);
      }
    })
  }

  protected readonly claimReq = claimReq;
}
