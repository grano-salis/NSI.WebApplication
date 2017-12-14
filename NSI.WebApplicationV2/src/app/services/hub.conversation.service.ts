import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { IConversation } from "../pages/conversations/Models/conversations";
import { HubConnection } from '@aspnet/signalr-client';
import { IParticipant } from "../pages/conversations/Models/participant";
import { IMessage } from "../pages/conversations/Models/message";

@Injectable()
export class HubConversationService {

    

    private _url: string;
    private _hubConnection: HubConnection;

    public participantForMessageModel: IParticipant;
    public messageModel: IMessage;

    //Declared with component
    public messages: IMessage[] = [];
    public participants: IParticipant[] = [];
    public loggedUserId: number;
    public newMessage: string;
    public conversationId: number;
    
    constructor() {
        this._url = environment.serverUrl;
        this.init();
    }

    private getParticipanWithUserId(id: number): IParticipant {
        let participanto = this.participants;
        let participant = this.participants.find(x => x.user.id == id);
        return participant;
    }


    public sendMessage(newMessage: string, conversationId: number, loggedUserId: number): void {
        let participant = this.getParticipanWithUserId(this.loggedUserId);
        this._hubConnection.invoke('Send', this.newMessage, this.conversationId, this.loggedUserId, participant.participantId);
    }


    private createMessageModel(message: string, activeConversationId: number, loggedUserId: number, participant: IParticipant): void {
        this.messageModel = {};
        //This is temporary Id
        this.messageModel.messageId = 0;//this.messageListSelectedConversation[this.messageListSelectedConversation.length - 1].messageId + 1;
        this.messageModel.message = message;
        this.messageModel.createdByUser = participant.user;
        this.messageModel.dateCreated = Date.now().toString();
        this.messageModel.conversationId = activeConversationId;

    }

    private init() {
        this._hubConnection = new HubConnection(`${this._url}/chat`);

        this._hubConnection.on('Send', (data, activeConversationId, loggedUserId, participantId) => {

            this.participantForMessageModel = this.getParticipanWithUserId(loggedUserId);
            this.createMessageModel(data, activeConversationId, loggedUserId, this.participantForMessageModel);
            this.messages.push(this.messageModel);

        });

        this._hubConnection.start()
            .then(() => {
                console.log('Hub connection started')
            })
            .catch(err => {
                console.log('Error while establishing connection')
            });
    }

}