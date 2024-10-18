export type Modal = {
  title: string;
  content: any;
  button?: {
    accept: string;
    decline: string;
  };
};
