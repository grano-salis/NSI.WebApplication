import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { IConversation } from "../pages/conversations/Models/conversations";
import { IParticipant } from "../pages/conversations/Models/participant";

@Injectable()
export class ConversationService {

    private _url: string;

    constructor(private _http: HttpClient) {
        this._url = environment.serverUrl;
       }

       getConversations(id:number) : Observable<IConversation[]>{
        return this._http.get<IConversation[]>(`${this._url}/api/conversations/user/${id}`);
       }

       getParticipants(id:number): Observable<IParticipant[]>{
           return this._http.get<IParticipant[]>(`${this._url}/api/conversations/${id}/participants`);
       }
}