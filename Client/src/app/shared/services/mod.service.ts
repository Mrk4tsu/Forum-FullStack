import {inject, Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient, HttpParams} from '@angular/common/http';
import {ApiResult, PageResult} from '../model/api.interface';
import {Observable} from 'rxjs';
import {
  Mod,
  ModCombineRequest,
  ModDetail,
  ModDetailInternal,
  ModUpdateCombineRequest,
  React
} from '../model/mod.interface';

@Injectable({
  providedIn: 'root'
})
export class ModService {
  private http = inject(HttpClient)
  private apiUrl = `${environment.baseUrl}mod`;

  getMods(keyword: string = '', page: number = 1, pageSize: number = 10, tag: string = '', category: string = ''): Observable<ApiResult<PageResult<Mod>>> {
    const params = new HttpParams()
      .set('PageIndex', page.toString())
      .set('PageSize', pageSize.toString())
      .set('Username', tag)
      .set('Category', category)
      .set('KeyWord', keyword);
    return this.http.get<ApiResult<PageResult<Mod>>>(`${this.apiUrl}/all`, {params});
  }

  getModById(id: number): Observable<ApiResult<ModDetail>> {
    return this.http.get<ApiResult<ModDetail>>(`${this.apiUrl}/${id}`);
  }

  getModInternalById(id: number): Observable<ApiResult<ModDetailInternal>> {
    return this.http.get<ApiResult<ModDetailInternal>>(`${this.apiUrl}/internal/${id}`);
  }

  createMod(request: ModCombineRequest): Observable<ApiResult<number>> {
    return this.http.post<ApiResult<number>>(`${this.apiUrl}/create`, request);
  }

  updateMod(id: number, request: ModUpdateCombineRequest): Observable<ApiResult<number>> {
    return this.http.put<ApiResult<number>>(`${this.apiUrl}/update-mod`, request, {params: {id: id.toString()}});
  }

  deleteMod(id: number): Observable<ApiResult<any>> {
    return this.http.delete<ApiResult<any>>(`${this.apiUrl}/delete-mod?id=${id}`);
  }

  getReacts(id: number, page: number = 1, pageSize: number = 10): Observable<ApiResult<PageResult<React>>> {
    const params = new HttpParams()
      .set('PageIndex', page.toString())
      .set('PageSize', pageSize.toString());
    return this.http.get<ApiResult<PageResult<React>>>(`${this.apiUrl}/${id}/reacts`, {params});
  }

  createReact(request: any): Observable<ApiResult<any>> {
    return this.http.post<ApiResult<any>>(`${this.apiUrl}/create-reaction`, request);
  }

  deleteReact(id: number): Observable<ApiResult<any>> {
    return this.http.delete<ApiResult<any>>(`${this.apiUrl}/delete-reaction?id=${id}`);
  }

  getDateTimeAgo(date: string): string {
    const dateObject = new Date(date);
    const now = new Date();
    const dif = (now.getTime() - dateObject.getTime()) / 1000;

    if (dif < 60) return 'vài giây trước';
    if (dif < 3600) return `${Math.floor(dif / 60)} phút trước`;
    if (dif < 86400) return `${Math.floor(dif / 3600)} giờ trước`;
    if (dif < 604800) return `${Math.floor(dif / 86400)} ngày trước`;
    if (dif < 2592000) return `${Math.floor(dif / 604800)} tuần trước`;
    if (dif < 31536000) return `${Math.floor(dif / 2592000)} tháng trước`;
    return `${Math.floor(dif / 31536000)} năm trước`;
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
