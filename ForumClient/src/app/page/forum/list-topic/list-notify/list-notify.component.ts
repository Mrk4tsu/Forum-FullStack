import {ChangeDetectorRef, Component, inject, OnInit} from '@angular/core';
import {NgForOf} from "@angular/common";
import {TopicService} from '../../../../shared/services/topic.service';
import {Topic} from '../../../../shared/model/topic.interface';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-list-notify',
  imports: [
    NgForOf,
    RouterLink
  ],
  templateUrl: './list-notify.component.html',
  styleUrls:
    [
      './list-notify.component.css',
      '../../forum.component.css'
    ],
})
export class ListNotifyComponent implements OnInit {
  topicService = inject(TopicService)
  cdr = inject(ChangeDetectorRef)
  notifications: Topic[] = [];

  ngOnInit(): void {
    this.loadNotifications()
  }

  loadNotifications() {
    this.topicService.getNotification().subscribe({
      next: (response) => {
        if (response.success) {
          this.notifications = response.data;
          this.cdr.markForCheck();
        }
      },
      error: (error) => {
        console.error('Error loading notifications:', error);
      }
    });
  }
}
