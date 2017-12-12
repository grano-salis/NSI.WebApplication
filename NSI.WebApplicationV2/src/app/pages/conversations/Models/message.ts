import { IUser } from "./user";

export interface IMessage {
    messageId ? : number;
    message ? : string;
    dateCreated ? : string;
    conversationId ? : number;
    createdByUser ? : IUser;
}