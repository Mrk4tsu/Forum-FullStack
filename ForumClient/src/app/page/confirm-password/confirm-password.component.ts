import {Component, inject, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, ValidatorFn, Validators} from '@angular/forms';
import {AuthService} from '../../shared/services/auth.service';
import {ActivatedRoute, Router} from '@angular/router';
import {CommonModule} from '@angular/common';
import {FirstkeyPipe} from '../../shared/services/helper/firstkey.pipe';

@Component({
  selector: 'app-confirm-password',
  imports: [ReactiveFormsModule, CommonModule, FirstkeyPipe],
  templateUrl: './confirm-password.component.html',
  styleUrls: [
    './confirm-password.component.css',
    '../login/login.component.css',
  ]
})
export class ConfirmPasswordComponent implements OnInit {
  username: string = '';
  token: string = '';
  errorMessage: string = '';
  successMessage: string = '';
  isLoading: boolean = false;
  isSubmitted: boolean = false;
  countdown: number = 0;
  form = this.fb.group({
    newPassword: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', Validators.required]
  }, {validators: this.passwordMatchValidator()});

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,
  ) {
  }

  ngOnInit(): void {
    // Get username and token from query parameters
    this.route.queryParams.subscribe(params => {
      this.username = params['username'] || '';
      this.token = params['token'] || '';
    });
    console.log(this.username, this.token);
  }

  passwordMatchValidator(): ValidatorFn {
    return (control: AbstractControl): null => {
      const password = control.get('newPassword');
      const confirmPassword = control.get('confirmPassword');
      if (password && confirmPassword && password.value !== confirmPassword.value) {
        confirmPassword?.setErrors({passwordMismatch: true});
      } else {
        confirmPassword?.setErrors(null);
      }
      return null;
    }
  }

  hasDisplayError(controlName: string): Boolean {
    const control = this.form.get(controlName);
    return Boolean(control?.invalid) && (this.isSubmitted || Boolean(control?.touched));
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.isSubmitted = true;
      return;
    }
    const resetRequest = {
      username: this.username,
      token: encodeURIComponent(this.token),
      newPassword: this.form.value.newPassword,
      confirmPassword: this.form.value.confirmPassword
    };
    this.isLoading = true;
    this.authService.confirmPasswordReset(resetRequest).subscribe({
      next: (response) => {
        if (response.success) {
          this.isLoading = false;
          this.isSubmitted = false;
          this.successMessage = 'Đôing mật khẩu thành công!';
          this.errorMessage = '';
          this.form.reset();
          this.countdown = 5;
          const interval = setInterval(() => {
            this.countdown--;
            if (this.countdown === 0) {
              clearInterval(interval);
              this.router.navigate(['/login']).then();
            }
          }, 1000);
        } else {
          this.isLoading = false;
          this.isSubmitted = false;
          this.errorMessage = response.message || 'Thao tác không thành công. Vui lòng thử lại.';
          this.successMessage = '';
        }
      },
      error: (error) => {
        this.isLoading = false;
        this.isSubmitted = false;
        this.errorMessage = error.error?.message || 'Failed to reset password. Please try again.';
        this.successMessage = '';
      }
    })
  }
}
