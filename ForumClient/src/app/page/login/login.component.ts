import {Component, inject} from '@angular/core';
import {AuthService} from '../../shared/services/auth.service';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {Router, RouterLink} from '@angular/router';
import {SeoService} from '../../shared/services/seo.service';
import { TokenService } from '../../shared/services/token.service';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  authService = inject(AuthService)
  tokenService = inject(TokenService)
  seo = inject(SeoService)
  router = inject(Router)
  form = this.formBuilder.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
    clientId: [this.tokenService.clientId],
  })

  isSubmitted: boolean = false;
  isLoading: boolean = false;
  message: string = '';

  constructor(public formBuilder: FormBuilder) {
  }

  ngOnInit() {
    this.seo.updateSeo('Đăng nhập', null, null);
    this.tokenService.clearToken()
  }

  onLogin() {
    this.isSubmitted = true;
    if (this.form.valid) {
      this.isLoading = true;
      this.authService.login(this.form.value).subscribe({
        next: (res) => {
          this.isLoading = false;
          if (res.success) {
            this.router.navigate(['/']);
          } else this.message = res.message || 'Đăng nhập thất bại';
        }, error: (err) => {
          this.isLoading = false;
          this.message = 'Tài khoản hoặc mât khẩu không đúng';
        }
      })
    }
  }
}
