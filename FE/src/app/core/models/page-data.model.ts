export type PageData<T = any> = {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: T;
};
