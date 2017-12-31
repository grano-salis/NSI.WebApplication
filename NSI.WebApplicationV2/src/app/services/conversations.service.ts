import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { IConversation } from "../pages/conversations/Models/conversations";
import { IParticipant } from "../pages/conversations/Models/participant";
import { Response } from '@angular/http/src/static_response';
import { IUser } from '../pages/conversations/Models/user';


@Injectable()
export class ConversationService {

    private _url: string;

    constructor(private _http: HttpClient) {
        this._url = environment.serverUrl;
       }

       getConversations(id:number) : Observable<IConversation[]>{
        return this._http.get<IConversation[]>(`${this._url}/api/conversations/user/${id}`);
       }

       getSystemUsers() : Observable<IUser[]>{
           return this._http.get<IUser[]>(`${this._url}/api/conversations/users`);
       }

       getParticipants(id:number): Observable<IParticipant[]>{
           return this._http.get<IParticipant[]>(`${this._url}/api/conversations/${id}/participants`);
       }

       getParticipantById(id: number): Observable<IParticipant>
       {
           return this._http.get<IParticipant>(`${this._url}/api/participants/${id}`);
       }

       createConversation(loggedUserId : number, usersToParticipants : number[], conversationName : string)
       {

            let payload = {
                loggedUserId: loggedUserId,
                usersToParticipants: usersToParticipants,
                conversationName: conversationName
            }
           return this._http.post<IConversation>(`${this._url}/api/conversations`, payload);
            
       }
       addNewParticipantToExistingConversation(convId : number, participants : number[])
       {
           let payload = {
               conversationId : convId,
               usersToParticipant : participants
           }
           return this._http.post(`${this._url}/api/conversations/${convId}/participants`, payload);
       }      
}