import { IUser } from "./user";

export interface IParticipant {
    participantId: number;
    isSnoozed: boolean;
    isDeleted: boolean;
    dateCreated: string;
    lastSeenTime: string;
    conversationId: number;
    user: IUser;
}