import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { DocumentCategory } from '../pages/document-category/document-category.model';

@Injectable()
export class DocumentCategoryService {

  private readonly _url: string;
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/documentCategory';
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  getDocumentCategories(params?: any): Observable<any> {
    return this.http.get(`${this._url}`);
  }
  
  getDocumentCategoryById(id: number): Observable<any>{
    
        return this.http.get(this._url + "/" + id);
      }

      
      postDocumentcategory(documentCategory: DocumentCategory): Observable<any> {
        const body = JSON.stringify(documentCategory);
        const headers = new HttpHeaders({'Content-Type': 'application/json'});
        return this.http.post(`${this._url}`, body, {headers: headers});
    
      }  

         
  deleteDocumentCategory(params?: number): Observable<any> {
    return this.http.delete(`${this._url}` + params.toString());
  }


}
