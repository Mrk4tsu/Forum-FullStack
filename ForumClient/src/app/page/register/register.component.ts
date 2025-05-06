import {ChangeDetectorRef, Component, inject, PLATFORM_ID} from '@angular/core';
import {CommonModule, isPlatformBrowser, NgIf} from '@angular/common';
import {AbstractControl, FormBuilder, ReactiveFormsModule, ValidatorFn, Validators} from '@angular/forms';
import {AuthService} from '../../shared/services/auth.service';
import {Router} from '@angular/router';
import {BehaviorSubject} from 'rxjs';
import {CaptchaComponent} from '../../shared/component/captcha/captcha.component';
import {SeoService} from '../../shared/services/seo.service';
import {FirstkeyPipe} from '../../shared/services/helper/firstkey.pipe';

@Component({
  selector: 'app-register',
  imports: [
    ReactiveFormsModule,
    CommonModule,
    CaptchaComponent,
    FirstkeyPipe
  ],
  templateUrl: './register.component.html',
  styleUrls: [
    './register.component.css',
    '../login/login.component.css'
  ]
})
export class RegisterComponent {
  authService = inject(AuthService)
  router = inject(Router)
  isBrowser = isPlatformBrowser(inject(PLATFORM_ID))
  seo = inject(SeoService)
  cdr = inject(ChangeDetectorRef)
  private captchaSubject = new BehaviorSubject<string | null>(null);
  captchaToken$ = this.captchaSubject.asObservable();

  form = this.formBuilder.group({
    username: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(10)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    avatarIndex: [518, Validators.required],
    confirmPassword: ['', Validators.required]
  }, {validators: this.passwordMatchValidator})

  isSubmitting: boolean = false;
  isLoading: boolean = false;
  message: string = '';
  avatar = [
    {url: 'assets/images/avatars/518.png', index: 518},
    {url: 'assets/images/avatars/519.png', index: 519},
    {url: 'assets/images/avatars/734.png', index: 734},
    {url: 'assets/images/avatars/520.png', index: 520},
    {url: 'assets/images/avatars/521.png', index: 521},
    {url: 'assets/images/avatars/522.png', index: 522},
    {url: 'assets/images/avatars/523.png', index: 523},
    {url: 'assets/images/avatars/524.png', index: 524},
  ];

  constructor(public formBuilder: FormBuilder) {
    if (!this.isBrowser) return
  }

  ngOnInit() {
    this.seo.updateSeo('Đăng ký', null, null);
  }

  onCaptchaVerified(token: string) {
    this.captchaSubject.next(token);
    this.cdr.detectChanges()
    console.log('CAPTCHA verified:', token);
  }

  onRegister() {
    this.isSubmitting = true;
    const registerData = {
      ...this.form.value,
      captchaToken: this.captchaSubject.value
    };
    if (this.form.valid) {
      this.isLoading = true;
      this.authService.register(registerData).subscribe({
        next: (res) => {
          this.isLoading = false;
          this.isSubmitting = false;
          if (res.success) {
            this.router.navigate(['/login']).then();
          } else {
            this.message = res.message || 'Đăng ký tài khoản thất bại';
          }
        },
        error: (err) => {
          this.isLoading = false;
          this.isSubmitting = false;
          this.message = err.error?.message || 'Đăng ký tài khoản thất bại';
          console.error('Register error', err);
        },
      });
    }
  }

  passwordMatchValidator(formGroup: any) {
    const password = formGroup.get('password').value;
    const confirmPassword = formGroup.get('confirmPassword').value;
    return password === confirmPassword ? null : {mismatch: true};
  }

  hasDisplayError(controlName: string): Boolean {
    const control = this.form.get(controlName);
    return Boolean(control?.invalid) && (this.isSubmitting || Boolean(control?.touched));
  }
}
