export type Result<T = any> = {
  success: boolean;
  data: T;
  messages: string;
  statusCode: number;
  errors: string[];
};
