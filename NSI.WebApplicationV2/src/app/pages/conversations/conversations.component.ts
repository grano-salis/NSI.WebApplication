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
import { element } from 'protractor';
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
    public _onlineUsers: string[] = [];
    public _systemUsers: IUser[] = [];

    public newMessage: string;
    public messageModel: IMessage;
    public activeConversationId: number;
    public participantForMessageModel: IParticipant;
    public whosTyping: string;
    public typingConvId: number;
    public isNewMessageClicked: boolean;
    public onStart: boolean;
    public newConversationName: string;
    public conversationCreatedBy: string;
    public newConversationNameAP: string;
    public directMessageAP: boolean;

    //multiselect
    dropdownList: any = [];
    selectedItems: any[] = [];
    dropdownSettings = {};
    selectedIds: number[] = [];

    dropdownListAP: any = [];
    selectedItemsAP: any[] = [];
    selectedIdsAP: number[] = [];

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
        this.onStart = true;
        this.newConversationName = "";
        this.newConversationNameAP = "";
        this.directMessageAP = false;

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
                this.conversationCreatedBy = this.findConversationCreator();
            });
        this._hubConversationService.messages = this.messageListSelectedConversation;
        this._hubConversationService.conversationId = this.activeConversationId;
        this._hubConversationService.loggedUserId = this.loggedUserId;
        this.onStart = false;
    }

    public isMessageSender(loggedUserId: number, senderUserId: number): boolean {
        return loggedUserId == senderUserId;
    }

    public checkIfConversationExists(): boolean {
        let selectedParticipants = this.selectedIds;
        selectedParticipants.push(this.loggedUserId);
        selectedParticipants.sort();

        if (selectedParticipants.length > 2)
            return false;

        for (let i = 0; i < this._conversations.length; i++) {
            let participantIds = [];
            for (let j = 0; j < this._conversations[i].participant.length; j++) {
                participantIds.push(this._conversations[i].participant[j].user.id);
            }
            participantIds.sort();

            let areEqual = this.equalArrays(selectedParticipants, participantIds);
            if (areEqual) {
                this.activeConversationId = this._conversations[i].conversationId;
                this._hubConversationService.conversationId = this.activeConversationId;
                this.selectedIds = [];
                this.selectedItems = [];
                this.loadMessages(this.activeConversationId);
                return true;
            }

        }
        return false;
    }

    public equalArrays(arr1: Number[], arr2: Number[]): boolean {
        if (arr1.length !== arr2.length)
            return false;
        for (let i = 0; i < arr1.length; i++)
            if (arr1[i] !== arr2[i])
                return false;

        return true;
    }

    public sendMessage(): void {
        this._hubConversationService.sendMessage(this.newMessage, this.activeConversationId, this.loggedUserId);
        this.newMessage = "";
        this.whosTyping = "";
        this._hubConversationService.typingUsername = "";
    }

    public createNewConversation(): void {
        this.messageListSelectedConversation = [];
        this.isNewMessageClicked = true;
    }

    public createConversation(): void {
        this.selectedIds = [];
        this.selectedItems.forEach(element => {
            this.selectedIds.push(element.id);
        });

        if (!this.checkIfConversationExists()) {
            this._conversationService.createConversation(this.loggedUserId, this.selectedIds, this.newConversationName)
                .subscribe(data => {

                    if (data.participant.length <= 2) {
                        let convName = "";
                        for (let i = 0; i < data.participant.length; i++) {
                            if (data.participant[i].user.id != this.loggedUserId) {
                                convName = data.participant[i].user.firstName + ' ' + data.participant[i].user.lastName;

                            }
                        }
                        data.conversationName = convName;
                    }
                    this._conversations.push(data);

                    this.selectedIds = [];
                    this.newConversationName = "";
                    this.selectedItems = [];
                })
        }
    }

    public prepareforAdding(): void {
        this.selectedIdsAP = [];
        this.selectedItemsAP = [];
        let conv = this._conversations.find(c => c.conversationId == this.activeConversationId);
        let idsToHide = [];
        this.dropdownListAP = [];
        for (let i = 0; i < conv.participant.length; i++) {
            idsToHide.push(conv.participant[i].user.id);
        }

        for (let i = 0; i < this._systemUsers.length; i++) {
            let exists = false;
            for (let j = 0; j < idsToHide.length; j++) {
                if (this._systemUsers[i].id == idsToHide[j])
                    exists = true;

            }
            if (!exists)
                this.dropdownListAP.push({ "id": this._systemUsers[i].id, "itemName": this._systemUsers[i].firstName + ' ' + this._systemUsers[i].lastName });
        }
        if (conv.participant.length <= 2)
            this.directMessageAP = true;



        $("#myModal").modal('show');
    }

    public addNewParticipant(): void {
        let conv = this._conversations.find(con => con.conversationId == this.activeConversationId);
        if (conv.participant.length <= 2) {

            this.selectedIdsAP = [];
            this.selectedItemsAP.forEach(element => {
                this.selectedIdsAP.push(element.id);
            });
            for (let i = 0; i < conv.participant.length; i++) {
                this.selectedIdsAP.push(conv.participant[i].user.id);
            }
            this._conversationService.createConversation(this.loggedUserId, this.selectedIdsAP, this.newConversationNameAP)
                .subscribe(data => {
                    this._conversations.push(data);
                    this.selectedIdsAP = [];
                    this.newConversationNameAP = "";
                    this.selectedItemsAP = [];
                });

        }
        else {

            this.selectedIdsAP = [];
            this.selectedItemsAP.forEach(element => {
                this.selectedIdsAP.push(element.id);
            });

            this._conversationService.addNewParticipantToExistingConversation(this.activeConversationId, this.selectedIdsAP)
                .subscribe(data => {

                    for (let i = 0; i < data.length; i++) {
                        conv.participant.push(data[i]);
                        let loggedUserPart = conv.participant.find(p => p.user.id == this.loggedUserId);
                        this.newMessage = "!!!INFO!!! ";
                        this.newMessage += loggedUserPart.user.firstName + ' ' + loggedUserPart.user.lastName;
                        this.newMessage += " added ";
                        this.newMessage += data[i].user.firstName + ' ' + data[i].user.lastName;
                        this.newMessage += " to conversation.";
                        this.sendMessage();
                        this.loadMessages(conv.conversationId);

                    }
                });
        }

    }


    public checkIfUserIsOnline(id: number): boolean {
        let ID = id.toString();
        let index = this._onlineUsers.findIndex(x => x == ID);
        return index != -1;
    }

    public isGroupConversation(): boolean {
        return this.selectedItems.length > 1;
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

    private findConversationCreator(): string {
        let creatorId = this._conversations.find(conv => conv.conversationId == this.activeConversationId).userId;
        let creator = this._participants.find(part => part.user.id == creatorId);
        return creator.user.firstName + ' ' + creator.user.lastName;
    }

    public validateNewMessageLength(): boolean {
        return (this.newMessage && this.newMessage.length > 0);
    }

    public validateCreateConversation(): boolean {

        if (this.selectedItems.length >= 2)
            return this.newConversationName.length > 0
        return this.selectedItems.length > 0;
    }

    public validateCreateConversationAP(): boolean {
        if (this.activeConversationId) {
            let conv = this._conversations.find(c => c.conversationId == this.activeConversationId);
            if (conv.participant.length <= 2) {
                return this.newConversationNameAP.length > 0 && this.selectedItemsAP.length > 0;
            }
            else {
                return this.selectedItemsAP.length > 0;
            }
        }
        return false;
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
            this._hubConversationService.whoIsTyping(user.user.userName, this.activeConversationId);
        }

    }

    ngAfterContentChecked(): void {
        this.whosTyping = this._hubConversationService.getWhosTyping();
        this.typingConvId = this._hubConversationService.getTypingConversationId();
        this._hubConversationService.getOnlineUsersList()
            .subscribe(
            onlineUsers => {
                this._onlineUsers = onlineUsers;
            });
    }

    ngAfterViewChecked(): void {

    }

    ngOnInit() {

        this._conversationService.getConversations(this.loggedUserId)
            .subscribe(
            conversations => {
                this._conversations = conversations;
                this.determineConvName();
            });

        this._conversationService.getSystemUsers()
            .subscribe(users => {
                this._systemUsers = users;
                for (let i = 0; i < this._systemUsers.length; i++) {
                    if (this._systemUsers[i].id == this.loggedUserId)
                        continue;
                    this.dropdownList.push({ "id": this._systemUsers[i].id, "itemName": this._systemUsers[i].firstName + ' ' + this._systemUsers[i].lastName });
                }
            });

        this.selectedItems = [

        ];
        this.dropdownSettings = {
            singleSelection: false,
            text: 'Select participant(s)',
            enableSearchFilter: true,
            classes: 'multiSelectDrop col-md-6',
            enableCheckAll: false,
            maxHeight: 200,
            searchPlaceholderText: 'Type the name of a person'
            //badgeShowLimit: 3 - uncomment if we'd like to support limitation on displaying selected items, adding '+x' 
        };
    }
    onItemSelect(item: any) {

    }
    OnItemDeSelect(item: any) {

    }
    onSelectAll(items: any) {

    }
    onDeSelectAll(items: any) {

    }
    onItemSelectAP(item: any) {

    }
    OnItemDeSelectAP(item: any) {

    }
    onSelectAllAP(items: any) {

    }
    onDeSelectAllAP(items: any) {

    }
}
