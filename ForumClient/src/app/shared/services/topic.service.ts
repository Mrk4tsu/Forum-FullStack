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

  getAllTopics(pageIndex: number = 1, pageSize: number = 10): Observable<ApiResult<PageResult<Topic>>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    return this.http.get<ApiResult<PageResult<Topic>>>(this.apiTopicUrl + '/list', {params});
  }

  getDetailTopic(id: number): Observable<ApiResult<TopicDetail>> {
    return this.http.get<ApiResult<TopicDetail>>(`${this.apiTopicUrl}/get-post?id=${id}`);
  }

  getReplies(topicId: number, pageIndex: number = 1, pageSize: number = 10): Observable<ApiResult<PageResult<Reply>>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    return this.http.get<ApiResult<PageResult<Reply>>>(`${this.apiTopicUrl}/get-replies?postId=${topicId}`, {params});
  }

  createTopic(form: any): Observable<ApiResult<Topic>> {
    return this.http.post<ApiResult<Topic>>(this.apiTopicUrl + '/create-post', form);
  }

  updateTopic(id: number, form: any): Observable<ApiResult<any>> {
    return this.http.put<ApiResult<any>>(this.apiTopicUrl + `/update-post?id=${id}`, form);
  }

  createReply(form: any): Observable<ApiResult<any>> {
    return this.http.post<ApiResult<any>>(this.apiTopicUrl + '/create-reply', form);
  }

  deleteReply(id: string): Observable<ApiResult<any>> {
    return this.http.delete<ApiResult<any>>(`${this.apiTopicUrl}/delete-reply?id=${id}`);
  }

  getNotification(): Observable<ApiResult<Topic[]>> {
    return this.http.get<ApiResult<Topic[]>>(`${this.apiTopicUrl}/get-notify`);
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
