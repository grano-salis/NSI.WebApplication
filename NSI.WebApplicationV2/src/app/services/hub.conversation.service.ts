import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { IConversation } from "../pages/conversations/Models/conversations";
import { HubConnection } from '@aspnet/signalr-client';
import { IParticipant } from "../pages/conversations/Models/participant";
import { IMessage } from "../pages/conversations/Models/message";
import { IUser } from '../pages/conversations/Models/user';

@Injectable()
export class HubConversationService {

    private _url: string;
    private _hubConnection: HubConnection;
    public async: IMessage;
    public participantForMessageModel: IParticipant;
    public messageModel: IMessage;
    public typingUsername: string;
    public typingConversationId : number;
    //Declared with component
    public messages: IMessage[] = [];
    public participants: IParticipant[] = [];
    public loggedUserId: number;
    public newMessage: string;
    public conversationId: number;
    public onlineUsers: string[];

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
        this._hubConnection.invoke('Send', newMessage, this.conversationId, this.loggedUserId, participant.participantId);
    }

    public getOnlineUsersList(): Observable<string[]> {
        return Observable.of(this.onlineUsers);
    }

    public getWhosTyping(): string {
        return this.typingUsername;
    }

    public getTypingConversationId() : number {
        return this.typingConversationId;
    }

    public whoIsTyping(user: string, conversationId: number): void {
        this._hubConnection.invoke('whoIsTyping', user, conversationId);
    }

    public persistLoginForOnlineStatus(username: string): void {
        this._hubConnection.invoke('persistForOnlineStatus', 'amirl');
    }

    public checkOnlineStatus(username: string): boolean {
        let result = true;
        this._hubConnection.invoke('checkOnlineStatusForUser', username).then((data) => { result = data; });
        return result;
    }

    public createConversation(loggedUserId: number, usersToParticipants: number[], conversationName: string):boolean {
        this._hubConnection.invoke('CreateConversation', loggedUserId, usersToParticipants, conversationName);
        return true;
    }

    private createMessageModel(message: string, activeConversationId: number, loggedUserId: number, participant: IParticipant): void {
        this.messageModel = {}; //This is temporary Id
        this.messageModel.messageId = 0;
        this.messageModel.message = message;
        this.messageModel.createdByUser = participant.user;
        this.messageModel.dateCreated = Date.now().toString();
        this.messageModel.conversationId = activeConversationId;
    }

    private init() {
        this._hubConnection = new HubConnection(`${this._url}/chat`);
        this._hubConnection.on('Send', (data, activeConversationId, loggedUserId, participantId) => {

            if (this.conversationId == activeConversationId) {
                this.participantForMessageModel = this.getParticipanWithUserId(loggedUserId);
                this.createMessageModel(data, activeConversationId, loggedUserId, this.participantForMessageModel);
                this.messages.push(this.messageModel);
                this.typingUsername = "";
            }
        });

        this._hubConnection.on('whoIsTyping', (data, conversationId) => {
            this.typingUsername = data;
            this.typingConversationId = conversationId;
        });

        this._hubConnection.on('setOnlineUsers', data => {
            this.onlineUsers = data;            
        });

        this._hubConnection.start()
            .then(() => {
                console.log('Hub connection started');
                this._hubConnection.invoke('persistForOnlineStatus', this.loggedUserId.toString());
            })
            .catch(err => {
                console.log('Error while establishing connection');
            });
    }
}