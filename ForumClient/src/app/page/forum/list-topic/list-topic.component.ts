import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { TopicService } from '../../../shared/services/topic.service';
import { BehaviorSubject, map } from 'rxjs';
import { Topic } from '../../../shared/model/topic.interface';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../shared/services/auth.service';
import { SeoService } from '../../../shared/services/seo.service';
import { PaginationComponent } from '../../../shared/component/pagination/pagination.component';
import {CkeditorConfigService} from '../../../shared/services/ckeditor-config.service';
import {CKEditorModule} from '@ckeditor/ckeditor5-angular';
import {ListNotifyComponent} from './list-notify/list-notify.component';

@Component({
  selector: 'app-list-topic',
  imports: [CommonModule, RouterLink, ReactiveFormsModule, PaginationComponent, CKEditorModule, ListNotifyComponent],
  templateUrl: './list-topic.component.html',
  styleUrls: [
    './list-topic.component.css',
    '../forum.component.css'
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ListTopicComponent implements OnInit, OnChanges {
  @Input() page: number | null = null;

  topicService = inject(TopicService)
  authService = inject(AuthService)
  seo = inject(SeoService)
  cdr = inject(ChangeDetectorRef)
  router = inject(Router)
  ckEditor = inject(CkeditorConfigService);

  paginationData = {
    currentPage: 1,
    totalPages: 1,
    totalRecords: 0
  };

  private topicSubject = new BehaviorSubject<Topic[]>([]);
  topics$ = this.topicSubject.asObservable();
  form = this.formBuilder.group({
    title: ['', Validators.required],
    content: ['', Validators.required]
  })

  isAuthorize = this.authService.isAuthenticated();
  isLoading: boolean = false;
  message: string = '';

  constructor(public formBuilder: FormBuilder) {
  }

  ngOnInit() {
    this.seo.updateSeo('Diễn đàn', null, null);
    this.loadTopics()
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['page']) {
      this.loadTopics();
    }
  }
  createTopic() {
    if (this.form.invalid) {
      this.message = 'Vui lòng kiểm tra lại thông tin';
      return;
    }
    this.isLoading = true;
    this.topicService.createTopic(this.form.value).subscribe({
      next: data => {
        if (data.success) {
          this.loadTopics()
          this.form.reset()
        } else {
          this.message = data.message;
        }
      }, error: err => {
        this.message = 'Có lỗi xảy ra, vui lòng thử lại sau';
      },
      complete: () => {
        this.isLoading = false;
        this.cdr.detectChanges()
      }
    })
  }

  loadTopics() {
    this.topicService.getAllTopics(this.page!).subscribe({
      next: result => {
        if (result.success) {
          this.topicSubject.next(result.data.items);
          this.paginationData = {
            currentPage: result.data.pageIndex,
            totalPages: Math.ceil(result.data.totalRecords / result.data.pageSize),
            totalRecords: result.data.totalRecords
          };
          this.cdr.markForCheck()
        }
      }
    })
  }
  onPageChange(newPage: number) {
    const queryParams: any = { page: newPage };
    this.router.navigate(['/'], {
      queryParams,
      queryParamsHandling: 'merge'
    });
  }
}
