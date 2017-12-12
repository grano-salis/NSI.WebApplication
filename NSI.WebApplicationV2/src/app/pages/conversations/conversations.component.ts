import 'rxjs/add/operator/map';
import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { IConversation } from "./Models/conversations";
import { ConversationService } from "../../services/conversations.service";
import { SlicePipe } from '@angular/common';
import { IMessage } from "./Models/message";
import { HubConversationService } from "../../services/hub.conversation.service";
import { IParticipant } from "./Models/participant";
import { ActivatedRoute } from '@angular/router';
import { IUser } from "./Models/user";


@Component({
    selector: 'app-conversations',
    templateUrl: './conversations.component.html',
    styleUrls: ['./conversations.component.scss']
})
export class ConversationsComponent implements OnInit{
    

    public async: any;
    public messageListSelectedConversation : IMessage[] = [];
    public loggedUserId : number;
    public _conversations: IConversation[] = [];
    public _participants: IParticipant[];
    public newMessage: string;
    public lastMessage:string;
    public messageModel:IMessage; 

    constructor(private _conversationService: ConversationService, private _hubConversationService: HubConversationService, private route: ActivatedRoute) {
    }

   
    public loadMessages( id: number): void{
        this.messageListSelectedConversation = this._conversations.filter((conversation: IConversation) => conversation.conversationId == id)[0].message
        this._conversationService.getParticipants(id)
        .subscribe(
            participants => {
                this._participants =  participants;
            }
        );
    }

    public isMessageSender(loggedUserId: number, senderUserId: number): boolean{
        return loggedUserId == senderUserId;
    }

    public sendMessage():void{   
        this._hubConversationService.sendMessage(this.newMessage,this.messageListSelectedConversation[0].conversationId,this.loggedUserId);
        
        //Create message model to refresh messages in current chat window
        this.createMessageModel();
        this.messageListSelectedConversation.push(this.messageModel);
        console.log(JSON.stringify(this.messageListSelectedConversation));
    }

    private getUserFromParticipants(id:number): IUser {
       let participant = this._participants.find(x => x.user.id == id);
       return participant.user;  
    }

    private createMessageModel(): void
    {
        this.messageModel = {};
        //This is temporary Id
        this.messageModel.messageId = this.messageListSelectedConversation[this.messageListSelectedConversation.length - 1].messageId + 1;
        this.messageModel.message = this.newMessage;
        this.messageModel.createdByUser = this.getUserFromParticipants(this.loggedUserId);
        this.messageModel.dateCreated = Date.now.toString();
        this.messageModel.conversationId = this.messageListSelectedConversation[0].conversationId;
    }

    ngOnInit() {

        this.route.params.subscribe(params => {
            this.loggedUserId = +params['id']; 
         });
        
         
        this._conversationService.getConversations(this.loggedUserId)
        .subscribe(
            conversations => {
                this._conversations = conversations;
            }
        )
    }
}
