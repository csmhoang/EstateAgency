export type User = {
  id: string;
  fullName: string;
  avatarUrl?: string;
  publicId?: string;
  phoneNumber: string;
  email: string;
  gender?: string;
  dateOfBirth?: Date;
  address?: string;
  createdAt?: Date;
  updatedAt?: Date;
  description?: string;
  roles?: string[];
};

export const Gender: { [key: string]: string } = {
  Male: 'Nam',
  Female: 'Nữ',
  Other: 'Khác',
};
