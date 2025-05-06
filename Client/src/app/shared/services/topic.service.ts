import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Observable} from 'rxjs';
import {Reply, Topic, TopicDetail} from '../model/topic.interface';
import {ApiResult, PageResult} from '../model/api.interface';

@Injectable({
  providedIn: 'root'
})
export class TopicService {
  http = inject(HttpClient)
  apiTopicUrl = environment.baseUrl + 'post'

  getDetailTopic(id: number): Observable<ApiResult<TopicDetail>> {
    return this.http.get<ApiResult<TopicDetail>>(`${this.apiTopicUrl}/get-post?id=${id}`);
  }

  createNotify(formData: any): Observable<ApiResult<any>> {
    return this.http.post<ApiResult<any>>(this.apiTopicUrl + '/create-notify', formData);
  }

  updateNotify(id: number, formData: any): Observable<ApiResult<any>> {
    return this.http.put<ApiResult<any>>(`${this.apiTopicUrl}/update-post?id=${id}`, formData);
  }

  getTimeAgo(date: string): string {
    const dateObj = new Date(date);
    const now = new Date();
    const diff = (now.getTime() - dateObj.getTime()) / 1000;

    if (diff < 60) return 'vài giây trước';
    if (diff < 3600) return `${Math.floor(diff / 60)} phút trước`;
    if (diff < 86400) return `${Math.floor(diff / 3600)} giờ trước`;
    if (diff < 604800) return `${Math.floor(diff / 86400)} ngày trước`;
    if (diff < 2592000) return `${Math.floor(diff / 604800)} tuần trước`;
    if (diff < 31536000) return `${Math.floor(diff / 2592000)} tháng trước`;
    return `${Math.floor(diff / 31536000)} năm trước`;
  }

  formatDateTime(dateInput: any): string {
    const date = dateInput?.toDate ? dateInput.toDate() : new Date(dateInput);
    return new Intl.DateTimeFormat('vi-VN', {
      day: 'numeric',
      month: 'long',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
      hour12: false
    }).format(date).replace('lúc', '');
  }

}
