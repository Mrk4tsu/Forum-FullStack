import {ChangeDetectorRef, Component, inject, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
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
  cdr = inject(ChangeDetectorRef)

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

  protected readonly claimReq = claimReq;
}
