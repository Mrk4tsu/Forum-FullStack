import {Component} from '@angular/core';
import {FirstkeyPipe} from '../../shared/services/helper/firstkey.pipe';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {NgIf, NgSwitchCase} from '@angular/common';
import {AuthService} from '../../shared/services/auth.service';

@Component({
  selector: 'app-change-password',
  imports: [
    FormsModule,
    NgIf,
    ReactiveFormsModule
  ],
  templateUrl: './change-password.component.html',
  styleUrls: [
    './change-password.component.css',
    '../login/login.component.css'
  ]
})
export class ChangePasswordComponent {
  changePasswordForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';
  isLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
  ) {
    this.changePasswordForm = this.fb.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmNewPassword: ['', Validators.required]
    }, {validators: this.passwordMatchValidator});
  }

  passwordMatchValidator(form: FormGroup) {
    return form.get('newPassword')?.value === form.get('confirmNewPassword')?.value
      ? null : {mismatch: true};
  }

  onSubmit(): void {
    if (this.changePasswordForm.invalid) {
      return;
    }

    this.isLoading = true;
    const changePasswordRequest = {
      currentPassword: this.changePasswordForm.get('currentPassword')?.value,
      newPassword: this.changePasswordForm.get('newPassword')?.value,
      confirmNewPassword: this.changePasswordForm.get('confirmNewPassword')?.value
    };
    this.authService.changePassword(changePasswordRequest).subscribe({
      next: (response: any) => {
        if (response.success) {
          this.isLoading = false;
          this.successMessage = 'Đổi mật khẩu thành công!';
          this.errorMessage = '';
          this.changePasswordForm.reset();
          return;
        } else {
          this.isLoading = false;
          this.errorMessage = response.message || 'Failed to change password. Please try again.';
          return;
        }
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.error?.message || 'Failed to change password. Please try again.';
        this.successMessage = '';
      }
    })
  }
}
