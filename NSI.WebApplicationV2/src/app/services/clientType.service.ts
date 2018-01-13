import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { ClientType } from '../pages/clientType/clientType.model';

@Injectable()
export class ClientTypeService {

  private readonly _url: string;
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/clientType';
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  getClientTypes(params?: any): Observable<any> {
    return this.http.get(`${this._url}`);
  }
  
  getClientTypeById(id: number): Observable<any>{
    
        return this.http.get(this._url + "/" + id);
      }

      
      postClientType(clientType: ClientType): Observable<any> {
        const body = JSON.stringify(clientType);
        const headers = new HttpHeaders({'Content-Type': 'application/json'});
        return this.http.post(`${this._url}`, body, {headers: headers});
    
      }  

         
  deleteClientType(params?: number): Observable<any> {
    return this.http.delete(`${this._url}` + params.toString());
  }


}
