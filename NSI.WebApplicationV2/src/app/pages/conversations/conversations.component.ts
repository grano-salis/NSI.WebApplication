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
import { HubConnection } from "@aspnet/signalr-client/dist/src";
import { environment } from "../../../environments/environment";
import { Event } from '@angular/router/src/events';
import { AfterContentChecked, AfterViewChecked } from '@angular/core/src/metadata/lifecycle_hooks';
declare var $: any;


@Component({
    selector: 'app-conversations',
    templateUrl: './conversations.component.html',
    styleUrls: ['./conversations.component.scss']
})
export class ConversationsComponent implements OnInit, AfterContentChecked, AfterViewChecked {
   
    




    public async: any;
    public messageListSelectedConversation: IMessage[] = [];
    public loggedUserId: number;
    public _conversations: IConversation[] = [];
    public _participants: IParticipant[];
    public _onlineUsers : string[] = [];
    
    public newMessage: string;
    public messageModel: IMessage;
    public activeConversationId: number;
    public participantForMessageModel: IParticipant;
    public whosTyping: string;
    public isNewMessageClicked: boolean;
    
    

    //multiselect
    dropdownList: any = [];
    selectedItems: any = [];
    dropdownSettings = {};



    //Fields for establishing connection on hub
    private _url: string;
    private _hubConnection: HubConnection;

    constructor(private _conversationService: ConversationService, private route: ActivatedRoute, private _hubConversationService: HubConversationService) {
        this._url = environment.serverUrl;

        this.route.params.subscribe(params => {
            this.loggedUserId = +params['id'];
            this._hubConversationService.loggedUserId = this.loggedUserId;
        });

       

        

        this.isNewMessageClicked = false;

    }


    public loadMessages(id: number): void {
        this.isNewMessageClicked = false;
        this.messageListSelectedConversation = this._conversations.filter((conversation: IConversation) => conversation.conversationId == id)[0].message
        this.activeConversationId = id;
        this._conversationService.getParticipants(id)
            .subscribe(
            participants => {
                this._participants = participants;
                this._hubConversationService.participants = this._participants;


            }
            );
        this._hubConversationService.messages = this.messageListSelectedConversation;
        this._hubConversationService.conversationId = this.activeConversationId;
        this._hubConversationService.loggedUserId = this.loggedUserId;
    }

    public isMessageSender(loggedUserId: number, senderUserId: number): boolean {
        return loggedUserId == senderUserId;
    }

    public sendMessage(): void {
        this._hubConversationService.sendMessage(this.newMessage, this.messageListSelectedConversation[0].conversationId, this.loggedUserId);
        this.newMessage = "";
        this.whosTyping = "";
        this._hubConversationService.typingUsername = "";
    }

    public createNewConversation(): void {

        this.messageListSelectedConversation = [];
        this.isNewMessageClicked = !this.isNewMessageClicked;

    }

    public checkIfUserIsOnline(id: number) : boolean
    {
        let ID = id.toString();
        let index =  this._onlineUsers.findIndex(x => x == ID);
        return index != -1;
    }





    private getUserFromParticipants(id: number): IUser {
        let participant = this._participants.find(x => x.user.id == id);
        return participant.user;
    }

    private getParticipanWithUserId(id: number): IParticipant {
        let participant = this._participants.find(x => x.user.id == id);
        return participant;
    }


    private determineConvName(): void {


        for (let i = 0; i < this._conversations.length; i++) {
            if (this._conversations[i].participant.length <= 2) {
                let userInfo = this._conversations[i].participant.find(x => x.user.id != this.loggedUserId);

                this._conversations[i].conversationName = userInfo.user.firstName + " " + userInfo.user.lastName;
            }
        }


    }

    public validateNewMessageLength(): boolean {
        return (this.newMessage && this.newMessage.length > 0);
    }


    keyPressEventHandler($event: KeyboardEvent) {
        if ($event.keyCode == 13) {
            if (this.newMessage.length > 0) {
                this.sendMessage();
                $event.preventDefault();
            }

        }
        else {
            let user = this._conversations.find(x => x.conversationId == this.activeConversationId).participant.find(y => y.user.id == this.loggedUserId);
            this._hubConversationService.whoIsTyping(user.user.userName);
        }


    }


    ngAfterContentChecked(): void {
        this.whosTyping = this._hubConversationService.getWhosTyping();
        this._hubConversationService.getOnlineUsersList()
        .subscribe(
            onlineUsers => {
                this._onlineUsers = onlineUsers;
                
            }
            
        )
        
    }

    ngAfterViewChecked(): void {
       
    
        
        
    }

    



    ngOnInit() {



        this._conversationService.getConversations(this.loggedUserId)
            .subscribe(
            conversations => {
                this._conversations = conversations;
                this.determineConvName();
            }
            )

        
           
           
           

        this.dropdownList = [
            { "id": 20, "itemName": "Amir Lisovac" },
            { "id": 19, "itemName": "Omar Dervišević" },
            { "id": 21, "itemName": "Ragib Smajić" },
            { "id": 22, "itemName": "Fadil Ademović" },            
            { "id": 23, "itemName": "Dino Alić" },
            { "id": 24, "itemName": "John Doe" },
            { "id": 25, "itemName": "Jane Doe" }
                
        ];
        this.selectedItems = [
                        
        ];
        this.dropdownSettings = {
            singleSelection: false,
            text: 'Select participant(s)',            
            enableSearchFilter: true,
            classes: 'multiSelectDrop col-md-12',
            enableCheckAll: false,            
            maxHeight: 200,
            searchPlaceholderText: 'Type the name of a person'//,
            //badgeShowLimit: 3
            
        };


    }

    onItemSelect(item:any){
        console.log(item);
        console.log(this.selectedItems);
    }
    OnItemDeSelect(item:any){
        console.log(item);
        console.log(this.selectedItems);
    }
    onSelectAll(items: any){
        console.log(items);
    }
    onDeSelectAll(items: any){
        console.log(items);
    }

}
