export type User = {
  id: string;
  userCode: number;
  fullName: string;
  avatarUrl?: string;
  publicId?: string;
  phoneNumber: string;
  email: string;
  gender?: string;
  dateOfBirth?: Date;
  address?: string;
  description?: string;
  roles?: string[];
};

export const Gender: { [key: string]: string } = {
  Male: 'Nam',
  Female: 'Nữ',
  Other: 'Khác',
};
