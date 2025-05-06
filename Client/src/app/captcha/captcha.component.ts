import {AfterViewInit, Component, EventEmitter, inject, Output, PLATFORM_ID} from '@angular/core';
import {environment} from '../../environments/environment';
import {isPlatformBrowser} from '@angular/common';

@Component({
  selector: 'app-captcha',
  imports: [],
  templateUrl: './captcha.component.html',
  styleUrl: './captcha.component.css'
})
export class CaptchaComponent implements AfterViewInit {
  isBrowser = isPlatformBrowser(inject(PLATFORM_ID))
  siteKey = environment.captchaKey;
  @Output() verified = new EventEmitter<string>();
  private widgetId: any;

  ngAfterViewInit() {
    if (!this.isBrowser) return
    // Thêm callback function vào window
    (window as any).onCaptchaSuccess = (token: string) => {
      this.verified.emit(token);
    };
  }

  initTurnstile() {
    if (!this.isBrowser) return
    (window as any).onCaptchaSuccess = (token: string) => {
      this.verified.emit(token);
    };

    if ((window as any).turnstile) {
      this.renderWidget();
    } else {
      setTimeout(() => this.initTurnstile(), 500);
    }
  }

  private renderWidget() {
    this.widgetId = (window as any).turnstile.render('.cf-turnstile', {
      sitekey: this.siteKey,
      callback: (token: string) => {
        this.verified.emit(token);
      }
    });
  }
}
