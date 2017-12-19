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
    public async: IMessage;

    public participantForMessageModel: IParticipant;
    public messageModel: IMessage;

    public typingUsername : string;

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
        this._hubConnection.invoke('Send', newMessage, this.conversationId, this.loggedUserId, participant.participantId);
        
    }

    public getWhosTyping(): string {
        return this.typingUsername;
    }

    public whoIsTyping(user : string) : void
    {
       this._hubConnection.invoke('whoIsTyping', user); 
    }

    public persistLoginForOnlineStatus(username : string) : void
    {
        this._hubConnection.invoke('persistForOnlineStatus', 'amirl');
    }

    public checkOnlineStatus(username: string) : boolean
    {
       let result = true;
       this._hubConnection.invoke('checkOnlineStatusForUser', username).then((data)=>{result = data;});
       return result;
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
            this.typingUsername="";

        });

        

        this._hubConnection.on('whoIsTyping', data => { 
            
            this.typingUsername = data;             
           
            
        });

       

        this._hubConnection.start()
            .then(() => {
                console.log('Hub connection started');  
                this._hubConnection.invoke('persistForOnlineStatus', this.loggedUserId.toString());

            })            
            .catch(err => {
                console.log('Error while establishing connection');
            })
            

        
    }

}