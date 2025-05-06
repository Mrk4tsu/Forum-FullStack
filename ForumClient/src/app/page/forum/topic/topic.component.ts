import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  inject,
  Input,
  OnChanges,
  OnInit, PLATFORM_ID,
  SimpleChanges
} from '@angular/core';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {CommonModule, isPlatformBrowser} from '@angular/common';
import {TopicService} from '../../../shared/services/topic.service';
import {Reply, TopicDetail} from '../../../shared/model/topic.interface';
import {BehaviorSubject} from 'rxjs';
import {AuthService} from '../../../shared/services/auth.service';
import {TokenService} from '../../../shared/services/token.service';
import {JwtPayload} from '../../../shared/model/token.interface';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {PaginationComponent} from '../../../shared/component/pagination/pagination.component';
import {SeoService} from '../../../shared/services/seo.service';
import {DomSanitizer, SafeHtml} from '@angular/platform-browser';
import hljs from 'highlight.js';

@Component({
  selector: 'app-topic',
  imports: [
    RouterLink,
    CommonModule,
    ReactiveFormsModule,
    PaginationComponent,
  ],
  templateUrl: './topic.component.html',
  styleUrl: './topic.component.css'
})
export class TopicComponent implements OnInit, OnChanges, AfterViewInit {
  @Input() id: number | null = null;
  @Input() page: number = 1;
  route = inject(ActivatedRoute)
  topicService = inject(TopicService)
  authService = inject(AuthService)
  tokenService = inject(TokenService)
  router = inject(Router)
  seo = inject(SeoService)
  cdr = inject(ChangeDetectorRef)
  isBrowser = isPlatformBrowser(inject(PLATFORM_ID))

  isAuthorize = this.authService.isAuthenticated();
  loading = false;
  sending = false;
  private topicSubject = new BehaviorSubject<TopicDetail>(new TopicDetail());
  topic$ = this.topicSubject.asObservable();
  private repliesSubject = new BehaviorSubject<Reply[]>([]);
  replies$ = this.repliesSubject.asObservable();

  userInfo: JwtPayload | null = null;
  form = this.formBuilder.group({
    postId: [this.id, Validators.required],
    content: ['', Validators.required],
    parentReplyId: [0],
  })
  paginationData = {
    currentPage: 1,
    totalPages: 1,
    totalRecords: 0
  };
  message: string = '';
  sanitizedContent: SafeHtml | null = null;

  constructor(private sanitizer: DomSanitizer, public formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.seo.updateSeo("Topic", null, null);
    if (this.id) {
      this.loadTopicDetail(this.id);
      this.loadReplies(this.id);
    }
    this.isAuthorize = this.authService.isAuthenticated();
    if (this.isAuthorize) {
      const accessToken = this.tokenService.accessToken;
      if (accessToken) {
        this.userInfo = this.tokenService.getUserInfoFromToken(accessToken);
      }
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['page']) {
      this.loadReplies(this.id!);
    }
  }

  ngAfterViewInit() {
    if (!this.isBrowser) return
    this.watchProductDetail();
  }

  watchProductDetail() {
    const checkData = setInterval(() => {

      if (this.topicSubject.value.content) {
        clearInterval(checkData);
        this.highlightCode();
      }
    },0);
  }

  highlightCode() {
    setTimeout(() => { // Đợi DOM cập nhật
      const blocks = document.querySelectorAll('pre code');
      blocks.forEach((block) => {
        hljs.highlightElement(block as HTMLElement);
      });
    }, 100);
  }

  onPageChange(newPage: number) {
    const queryParams: any = {page: newPage};
    this.router.navigate(['/topic', this.id], {
      queryParams,
      queryParamsHandling: 'merge'
    });
  }

  loadTopicDetail(topicId: number): void {
    this.loading = true;
    this.topicService.getDetailTopic(topicId).subscribe({
      next: (res) => {
        if (res.success) {
          this.sanitizedContent = this.sanitizer.bypassSecurityTrustHtml(res.data.content);
          this.topicSubject.next(res.data)
          this.loading = false;
        } else {
          this.router.navigate(['/404']);
          this.loading = false;
        }
      },
      error: (err) => {
        this.router.navigate(['/404']);
        this.loading = false;
      }
    });
  }

  loadReplies(topicId: number): void {
    this.loading = true;
    this.topicService.getReplies(topicId, this.page).subscribe({
      next: (result) => {
        this.paginationData = {
          currentPage: result.data.pageIndex,
          totalPages: Math.ceil(result.data.totalRecords / result.data.pageSize),
          totalRecords: result.data.totalRecords
        };
        this.repliesSubject.next(result.data.items)
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading replies:', err);
        this.loading = false;
      }
    });
  }

  createReply(): void {
    this.sending = true;
    if (this.id) {
      this.form.patchValue({postId: this.id});
    }
    const formValue = this.form.value;
    if (this.form.valid) {
      this.topicService.createReply(formValue).subscribe({
        next: (res) => {
          if (!res.success) {
            this.sending = false;
            this.message = res.message || 'Bạn bình luận quá nhanh, vui lòng thử lại sau';
            this.cdr.detectChanges();
            return;
          } else {
            console.log('Reply created successfully:', res);
            this.sending = false;
            this.form.reset();
            this.loadReplies(this.id!);
          }
        },
        error: (err) => {
          this.sending = false;
          console.error('Error creating reply:', err);
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }

  deleteReply(reply: Reply): void {
    if (this.authService.isAuthenticated()) {
      this.topicService.deleteReply(reply.id).subscribe({
        next: (res) => {
          if (res.success)
            this.loadReplies(this.id!);
        },
        error: (err) => {
          console.error('Error deleting reply:', err);
        }
      });
    }
  }
}
