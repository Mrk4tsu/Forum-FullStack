import {Directive, ElementRef, Input, OnInit} from '@angular/core';
import {TokenService} from '../token.service';

@Directive({
  selector: '[appHideContentIfClaimsNotMet]'
})
export class HideContentIfClaimsNotMetDirective implements OnInit {
  @Input("appHideContentIfClaimsNotMet") claimReq!: Function;

  constructor(private tokenService: TokenService, private elementRef: ElementRef) {
  }

  ngOnInit(): void {
    const claims = this.tokenService.getClaims();

    if (!this.claimReq(claims))
      this.elementRef.nativeElement.style.display = "none";
  }
}
