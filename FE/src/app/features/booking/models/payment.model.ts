export type Payment = {
  id: string;
  invoiceId: string;
  amount: number;
  paymentDate: Date;
  paymentMethod: string;
  status: string;
};

export const PaymentMethod: { [key: string]: string } = {
  CreditCard: 'Thẻ ngân hàng',
  Cash: 'Tiền mặt',
};