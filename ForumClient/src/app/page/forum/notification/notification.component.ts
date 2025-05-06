import {Component, inject, Input, OnInit} from '@angular/core';
import {TopicService} from '../../../shared/services/topic.service';
import {BehaviorSubject} from 'rxjs';
import {TopicDetail} from '../../../shared/model/topic.interface';
import {DomSanitizer, SafeHtml} from '@angular/platform-browser';
import {Router, RouterLink} from '@angular/router';
import {AsyncPipe, NgIf} from '@angular/common';
import {claimReq} from '../../../shared/services/helper/claimReq-utils';
import {
  HideContentIfClaimsNotMetDirective
} from '../../../shared/services/helper/hide-content-if-claims-not-met.directive';
import {AuthService} from '../../../shared/services/auth.service';
import {environment} from '../../../../environments/environment';

@Component({
  selector: 'app-notification',
  imports: [
    RouterLink,
    AsyncPipe,
    HideContentIfClaimsNotMetDirective,
    NgIf
  ],
  templateUrl: './notification.component.html',
  styleUrls: [
    './notification.component.css',
    '../topic/topic.component.css'
  ]
})
export class NotificationComponent implements OnInit {
  @Input() id: number | null = null;

  topicService = inject(TopicService)
  router = inject(Router)
  authService = inject(AuthService)

  private notifySubject = new BehaviorSubject<TopicDetail>(new TopicDetail());
  notify$ = this.notifySubject.asObservable();
  loading = false;
  sanitizedContent: SafeHtml | null = null;
  isAuthorize = false;

  constructor(private sanitizer: DomSanitizer) {
  }

  ngOnInit() {
    this.loadTopicDetail(this.id!);
    this.isAuthorize = this.authService.isAuthenticated();
    console.log('NotificationComponent initialized', this.isAuthorize);
  }

  loadTopicDetail(topicId: number): void {
    this.loading = true;
    this.topicService.getDetailTopic(topicId).subscribe({
      next: (res) => {
        if (res.success) {
          this.sanitizedContent = this.sanitizer.bypassSecurityTrustHtml(res.data.content);
          this.notifySubject.next(res.data)
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

  gotoUpdate(): void {
    const url = environment.baseDomain + 'edit-notify/' + this.id;
    if (url) {
      window.open(url);
    }
  }

  protected readonly claimReq = claimReq;
}
