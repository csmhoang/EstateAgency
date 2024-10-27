export type Pagination<T = any> = {
  PageIndex: number;
  PageSize: number;
  Count: number;
  Data: T;
};
