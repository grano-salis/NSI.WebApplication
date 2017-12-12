import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { IConversation } from "../pages/conversations/Models/conversations";
import { HubConnection } from '@aspnet/signalr-client';
import { IParticipant } from "../pages/conversations/Models/participant";

@Injectable()
export class HubConversationService {

    private _url: string;
    private _hubConnection: HubConnection;
    public messages: string[] = [];

    public sendMessage(newMessage:string,conversationId:number, loggedUserId : number): void {

        this._hubConnection.invoke('Send',newMessage,conversationId,loggedUserId);
    }

    constructor(){
        this._url = environment.serverUrl;
        this.init();
    }


    joinGroup(group: string): void {
        this._hubConnection.invoke('JoinGroup', group);
    }

    leaveGroup(group: string): void {
        this._hubConnection.invoke('LeaveGroup', group);
    }

    private init() {
        this._hubConnection = new HubConnection(`${this._url}/chat`);
        
                this._hubConnection.on('Send', (data: any) => {
                    const received = data;
                    this.messages.push(received);
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