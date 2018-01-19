import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { CaseCategory } from '../pages/caseCategory/caseCategory.model';

@Injectable()
export class CaseCategoryService {
  private readonly _url: string;
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/caseCategory';
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  getCaseCategories(): Observable<any> {
    return this.http.get(this._url);
  }
  getCaseCategoryById(id: number): Observable<any>{
    
        return this.http.get(this._url + '/' + id);
      }

      postCaseCategory(caseCategory: CaseCategory): Observable<any> {
        const body = JSON.stringify(caseCategory);
        const headers = new HttpHeaders({'Content-Type': 'application/json'});
        return this.http.post(`${this._url}`, body, {headers: headers});
        
    
      }  



      putCaseCategory(id: number, caseCategory:CaseCategory): Observable<any>{
        let body = JSON.stringify(caseCategory);
        let headers = new HttpHeaders({'Content-Type': 'application/json'});
    
        return this.http.put(this._url+'/'+ id, body, {headers:headers});
      }
      
      

      deleteCaseCategory(id:number): Observable<any> {
        const headers = new HttpHeaders({'Content-Type': 'application/json'});
        return this.http.delete(this._url+'/' + id,{headers:headers});
        
      }
   

}
