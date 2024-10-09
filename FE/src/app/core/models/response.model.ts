export type Response<T> = {
  success: boolean;
  data: T;
  messages: string;
  statusCode: number;
  errors: string[];
};
