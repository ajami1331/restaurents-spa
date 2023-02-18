export interface PageResponseModel<T> {
  data: T[];
  pageSize: number;
  pageCount: number;
  totalCount: number;
  pageNumber: number;
}
