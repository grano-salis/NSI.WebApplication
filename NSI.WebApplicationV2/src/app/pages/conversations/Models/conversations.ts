import { IMessage } from "./message";
import { IParticipant } from "./participant";

export interface IConversation {
    conversationId: number;
    conversationName: string;
    message: IMessage[];
    participant: IParticipant[];

}