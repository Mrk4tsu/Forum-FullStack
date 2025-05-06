import {Component, inject, Input, OnInit} from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {CKEditorModule} from '@ckeditor/ckeditor5-angular';
import {NgIf} from '@angular/common';
import {TopicService} from '../../shared/services/topic.service';
import {CkeditorConfigService} from '../../shared/services/ckeditor-config.service';

@Component({
  selector: 'app-update-notify',
  imports: [
    CKEditorModule,
    ReactiveFormsModule,
    NgIf,

  ],
  templateUrl: './update-notify.component.html',
  styleUrl: './update-notify.component.css'
})
export class UpdateNotifyComponent implements OnInit {
  @Input() id: number | null = null;
  topicService = inject(TopicService)
  private fb = inject(FormBuilder);
  private router = inject(Router);
  ckeditor = inject(CkeditorConfigService);
  modForm = this.fb.group({
    title: ['', [Validators.required]],
    content: ['', [Validators.required]],
  });
  isLoading = false;

  ngOnInit() {
    this.loadNotification(this.id!);
  }

  loadNotification(id: number): void {
    this.topicService.getDetailTopic(id).subscribe({
      next: (res) => {
        if (res.success) {
          this.modForm.patchValue({
            title: res.data.title,
            content: res.data.content
          });
        }
      },
      error: (err) => {
        this.router.navigate(['/404']);
      }
    })
  }

  onSubmit() {
    if (this.modForm.valid) {
      this.topicService.updateNotify(this.id!, this.modForm.value).subscribe({
        next: (res) => {
          if (res.success) {
            this.router.navigate(['/']);
          } else {
            alert(res.message);
          }
        },
        error: (err) => {
          alert('Có lỗi xảy ra, vui lòng thử lại sau');
        }
      })
    }
  }
}
