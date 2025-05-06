import {Component, inject, OnInit} from '@angular/core';
import {TopicService} from '../../shared/services/topic.service';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {SeoService} from '../../shared/services/seo.service';
import {CkeditorConfigService} from '../../shared/services/ckeditor-config.service';
import {CKEditorModule} from '@ckeditor/ckeditor5-angular';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-notify',
  imports: [
    ReactiveFormsModule,
    CKEditorModule,
    CommonModule
  ],
  templateUrl: './notify.component.html',
  styleUrl: './notify.component.css'
})
export class NotifyComponent implements OnInit {
  topicService = inject(TopicService);
  formBuilder = inject(FormBuilder);
  ckeditor = inject(CkeditorConfigService);

  seo = inject(SeoService);
  form = this.formBuilder.group({
    title: ['', Validators.required],
    content: ['', Validators.required]
  })
  isLoading: boolean = false;
  message: string = '';

  ngOnInit() {
    this.seo.updateSeoWithKeyword('Diễn đàn', null, null, null);
  }

  createNotify() {
    if (this.form.invalid) {
      this.message = 'Vui lòng kiểm tra lại thông tin';
      return;
    }
    this.isLoading = true;
    this.topicService.createNotify(this.form.value).subscribe({
      next: data => {
        if (data.success) {
          this.form.reset()
        } else {
          this.message = data.message;
        }
      }, error: err => {
        this.message = 'Có lỗi xảy ra, vui lòng thử lại sau';
      },
      complete: () => {
        this.isLoading = false;
      }
    })
  }
}
