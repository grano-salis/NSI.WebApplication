import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { FileType } from '../pages/file-type/file-type.model';

@Injectable()
export class FileTypeService {

  private readonly _url: string;
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/fileType';
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  getFileTypes(params?: any): Observable<any> {
    return this.http.get(`${this._url}`);
  }
  
  getFileTypeById(id: number): Observable<any>{
    
        return this.http.get(this._url + "/" + id);
      }

      
      postFileType(fileType: FileType): Observable<any> {
        const body = JSON.stringify(fileType);
        const headers = new HttpHeaders({'Content-Type': 'application/json'});
        return this.http.post(`${this._url}`, body, {headers: headers});
    
      }  

         
  deleteFileType(params?: number): Observable<any> {
    return this.http.delete(`${this._url}` + params.toString());
  }


}
