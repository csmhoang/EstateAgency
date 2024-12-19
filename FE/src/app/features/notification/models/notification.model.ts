export type Notice = {
    id?: string;
    receiverId: string;
    title: string;
    content: string;
    status?: string;
    createdAt?: Date;
    updatedAt?: Date;
}