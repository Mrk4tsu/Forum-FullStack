import {Component, inject, Input, OnInit, ViewChild} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {CKEditorComponent, CKEditorModule} from '@ckeditor/ckeditor5-angular';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import { ModService } from '../../shared/services/mod.service';
import { CkeditorConfigService } from '../../shared/services/ckeditor-config.service';
import {ModUpdateCombineRequest, UrlUpdateRequest} from '../../shared/model/mod.interface';
import {SeoService} from '../../shared/services/seo.service';

@Component({
  selector: 'app-update',
  imports: [CommonModule, ReactiveFormsModule, CKEditorModule, RouterLink],
  templateUrl: './update.component.html',
  styleUrls: [
    './update.component.css',
    '../upload/upload.component.css'
  ]
})
export class UpdateComponent implements OnInit {
  @Input() id: number | null = null;
  private modService = inject(ModService);
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private ckeditorService = inject(CkeditorConfigService);
  seo = inject(SeoService);

  @ViewChild(CKEditorComponent) editorComponent!: CKEditorComponent;

  readonly MAX_URLS = 3;
  modId!: number;
  isLoading = true;
  isSaving = false;
  isEditorReady = false;

  originalUrls: { id: number, url: string }[] = [];
  modForm = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(3)]],
    description: ['', [Validators.required, Validators.minLength(10)]],
    isPrivate: [false],
    categoryId: [1, [Validators.required, Validators.min(1)]],
    urls: this.fb.array([])
  });

  get editor() {
    return this.ckeditorService.Editor;
  }

  get editorConfig() {
    return this.ckeditorService.config;
  }

  get urlControls() {
    return (this.modForm.get('urls') as FormArray).controls as FormGroup[];
  }

  get canAddMoreUrls(): boolean {
    return this.urlControls.length < this.MAX_URLS;
  }

  ngOnInit(): void {
    this.seo.updateSeoWithKeyword('Cập nhật mod', null, null, null);
    this.modId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadModData();
  }

  loadModData(): void {
    if (!this.id) {
      this.router.navigate(['/']);
      return;
    }
    this.modService.getModInternalById(this.id).subscribe({
      next: (result) => {
        if (result.success && result.data) {
          const mod = result.data;
          this.isLoading = false;

          this.originalUrls = mod.urls ? [...mod.urls] : [];
          this.modForm.patchValue({
            name: mod.title,
            description: mod.description,
            isPrivate: mod.isPrivate,
            categoryId: mod.categoryId
          });

          // Clear existing URLs
          const urlArray = this.modForm.get('urls') as FormArray;
          urlArray.clear();

          // Add existing URLs
          if (mod.urls && mod.urls.length > 0) {
            mod.urls.forEach(url => {
              this.addUrl(url.url, url.id, false);
            });
          } else {
            // Add at least one empty URL field
            this.addUrl();
          }

          // Mark editor as ready after data is loaded
          setTimeout(() => {
            this.isEditorReady = true;
          }, 0);
        }
      },
      error: (err) => {
        console.error('Error loading mod:', err);
        this.isLoading = false;
      }
    });
  }

  addUrl(urlValue: string = '', urlId: number = 0, isEditable: boolean = true): void {
    const urlGroup = this.fb.group({
      url: [urlValue, Validators.required],
      id: [urlId],
      isEditable: [isEditable]
    });
    (this.modForm.get('urls') as FormArray).push(urlGroup);
  }

  toggleEditUrl(index: number): void {
    const urlGroup = this.urlControls[index];
    const isEditable = !urlGroup.value.isEditable;
    urlGroup.get('isEditable')?.setValue(isEditable);

    if (!isEditable) {
      // Khi tắt edit mode, khôi phục giá trị gốc nếu không thay đổi
      const originalUrl = this.originalUrls.find(u => u.id === urlGroup.value.id);
      if (originalUrl && urlGroup.value.url !== originalUrl.url) {
        urlGroup.patchValue({url: originalUrl.url});
      }
    }
  }

  removeUrl(index: number): void {
    (this.modForm.get('urls') as FormArray).removeAt(index);
  }

  onEditorReady(editor: any): void {
  }

  onSubmit(): void {
    if (this.modForm.invalid) return;

    const formValue = this.modForm.value;
    const urls = formValue.urls || [];
    // Phân loại URLs
    const newUrls = urls
      .filter((u: any) => u.id === 0 && u.url)
      .map((u: any) => u.url);
    const updatedUrls = urls
      .filter((u: any) => u.id !== 0 && u.url &&
        this.originalUrls.some(ou => ou.id === u.id && ou.url !== u.url))
      .map((u: any): UrlUpdateRequest => ({id: u.id, url: u.url}));
    const urlIdsToDelete = this.originalUrls
      .filter(ou => !urls.some((u: any) => u.id === ou.id))
      .map(ou => ou.id);

    const request: ModUpdateCombineRequest = {
      name: formValue.name!,
      description: formValue.description!,
      isPrivate: formValue.isPrivate || false,
      categoryId: Number(formValue.categoryId),
      newUrls: newUrls.length ? newUrls : undefined,
      updatedUrls: updatedUrls.length ? updatedUrls : undefined,
      urlIdsToDelete: urlIdsToDelete.length ? urlIdsToDelete : undefined
    };
    this.isSaving = true;
    this.modService.updateMod(this.modId, request).subscribe({
      next: (result) => {
        if (result.success && result.data) {
          this.router.navigate(['/download', result.data]);
        }
      },
      error: (err) => {
        console.error('Error updating mod:', err);
      }
    });
  }
}
