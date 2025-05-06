import {Component, inject} from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {AuthService} from '../../shared/services/auth.service';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-forgot-password',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './forgot-password.component.html',
  styleUrls:
    ['./forgot-password.component.css',
      '../login/login.component.css',
    ]
})
export class ForgotPasswordComponent {
  fb = inject(FormBuilder)
  authService = inject(AuthService)

  form = this.fb.group({
    username: ['', Validators.required],
    email: ['', Validators.required],
  })

  isLoading = false
  message = ''

  constructor() {
  }

  onSubmit() {
    if (this.form.valid) {
      this.isLoading = true
      this.authService.requestPasswordReset(this.form.value).subscribe({
        next: (res) => {
          if (res.success) {
            this.isLoading = false
            this.message = 'Vui lòng kiểm tra email của bạn để đặt lại mật khẩu.'
          } else {
            this.isLoading = false
            this.message = 'Có lỗi xảy ra, vui lòng thử lại sau.'
          }
        }, error: (err) => {
          this.isLoading = false
          this.message = 'Có lỗi xảy ra, vui lòng thử lại sau.' + err
        }
      })
    }
  }
}

