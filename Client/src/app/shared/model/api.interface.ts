export interface ApiResult<T> {
  success: boolean;
  message: string;
  data: T;
}

export interface PageResult<T> {
  pageIndex: number;
  pageSize: number;
  totalRecords: number;
  pageCount: number;
  items: T[];
}
