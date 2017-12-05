import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpHeaders, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {Document} from '../pages/documents/models/document.model';

@Injectable()
export class DocumentsService {

  private readonly _url: string;
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/api/documents';
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  getDocuments(params?: any): Observable<any> {
    return this.http.get(this._url, {headers: this.headers});
  }
  
  postDocument(document: Document): Observable<any> {
    let body = JSON.stringify(document);
    return this.http.post(this._url, body, {headers: this.headers});
  }
}
