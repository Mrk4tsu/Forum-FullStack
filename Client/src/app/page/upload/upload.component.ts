import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ModCombineRequest } from '../../shared/model/mod.interface';
import { ModService } from '../../shared/services/mod.service';
import { CkeditorConfigService } from '../../shared/services/ckeditor-config.service';
import { Router } from '@angular/router';
import { SeoService } from '../../shared/services/seo.service';
import { TokenService } from '../../shared/services/token.service';
import { JwtPayload } from '../../shared/model/token.interface';

@Component({
  selector: 'app-upload',
  imports: [CommonModule, CKEditorModule, ReactiveFormsModule],
  templateUrl: './upload.component.html',
  styleUrl: './upload.component.css'
})
export class UploadComponent {
  ckeditor = inject(CkeditorConfigService)
  tokenService = inject(TokenService)
  fb = inject(FormBuilder);
  modService = inject(ModService)
  router = inject(Router)
  seo = inject(SeoService)
  user: JwtPayload | null = null;
  ngOnInit() {
    this.seo.updateSeoWithKeyword('Tải lên mod', null, null, null);
    const token = this.tokenService.accessToken;
    if (token) {
      this.user = this.tokenService.getUserInfoFromToken(token);
    }
  }

  modForm = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(3)]],
    description: ['', [Validators.required, Validators.minLength(10)]],
    isPrivate: [false],
    categoryId: [1, [Validators.required, Validators.min(1)]],
    urls: this.fb.array([this.fb.control('')])
  });
  isLoading = false;
  readonly MAX_URLS = 3;

  get urls() {
    return this.modForm.get('urls') as any;
  }

  addUrl() {
    if (this.canAddMoreUrls) {
      this.urls.push(this.fb.control(''));
    }
  }

  removeUrl(index: number) {
    if (this.canRemoveUrl) {
      this.urls.removeAt(index);
    }
  }

  get canAddMoreUrls(): boolean {
    return this.urls.length < this.MAX_URLS;
  }

  get canRemoveUrl(): boolean {
    return this.urls.length > 1;
  }

  onSubmit() {
    if (this.modForm.invalid) return;

    const formValue = this.modForm.value;
    const request: ModCombineRequest = {
      name: formValue.name!,
      description: formValue.description!,
      isPrivate: formValue.isPrivate || false,
      categoryId: Number(formValue.categoryId),
      newUrls: formValue.urls?.filter((u): u is string => !!u) || []
    };
    this.isLoading = true;
    this.modService.createMod(request).subscribe({
      next: (result) => {
        if (result.success && result.data) {
          this.isLoading = false;
          this.router.navigate([`/mod/${this.user?.name}-${result.data}`]);
        }
      },
      error: (err) => {
        this.isLoading = false;
        console.error('Error creating mod:', err);
      }
    });
  }
}
