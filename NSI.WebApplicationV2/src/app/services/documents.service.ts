import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { RequestOptions, Headers } from "@angular/http";
import { Observable } from 'rxjs/Observable';
import { Observer } from 'rxjs/Observer';
import { Subject } from 'rxjs/Subject';

import {
  Document,
  DocumentDetails,
  DocumentQuery,
  DocumentFilter
} from '../pages/documents/models/index.model';

import { MDD } from '../pages/documents/models/mockDocumentDetails';

@Injectable()
export class DocumentsService {
  newFilterEvent = new Subject();
  chosenFilterEvent = new Subject<DocumentFilter>();
  filterList: string[];

  private readonly filterListFull: string[];    
  private readonly _url: any;
  private headers = new HttpHeaders();

  documentAdded = new Subject<Document>();
  documentUpdated = new Subject<{ index: number, document: Document }>();
  documentUpdatingRequested = new Subject();

  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
    this._url = {
      'documents': environment.serverUrl + '/api/documents/',
      'cases': environment.serverUrl + '/api/cases/',
      'categories': environment.serverUrl + '/api/categories/'
      };
    
    this.filterListFull = this.generateListOfFilters();
    this.filterList = this.generateListOfFilters();

    this.chosenFilterEvent.subscribe((documentFilter: DocumentFilter) => this.onFilterChangeDetected(documentFilter));
  }

  getDocuments(): Observable<DocumentDetails[]> {
    // return Observable.create( (observer: Observer<DocumentDetails[]>) => {
    //   observer.next(MDD);
    //   observer.complete();
    // });
    return this.http.get<DocumentDetails[]>(this._url.documents, {headers: this.headers});
  }

  getDocumentsWithPaging(queryModel: DocumentQuery): Observable<any> {
    let body = JSON.stringify(queryModel);
    return this.http.post(this._url.documents, body, {headers: this.headers});
  }

  getDocumentById(documentId: number): Observable<any> {
    return this.http.get(this._url.documents + documentId, {headers: this.headers});    
  }
  
  postDocument(document: Document): Observable<any> {
    let body = JSON.stringify(document);
    return this.http.post(this._url.documents, body, {headers: this.headers});
  }

  putDocument(index: number, document: Document): Observable<any> {
    let body = JSON.stringify(document);
    return this.http.post(this._url.documents, body, {headers: this.headers});
  }

  deleteDocument(documentId: number): Observable<any> {
    return this.http.post(this._url.documents + documentId, {headers: this.headers});
  }

  getCaseList(): any {
    return [];
  }

  getCategoryList(): any {
    return [];
  }

  onFilterChangeDetected(filterChange: DocumentFilter): void {
    if (filterChange.type == "default") {
      this.filterList.splice(0, 1);
    }
    else if (filterChange.type == "add") 
    {
      for (let i = this.filterList.length - 1; i >= 0; i--) {
        if (this.filterList[i] === filterChange.value) {
          this.filterList.splice(i, 1);
          break;
        }
      }
    }
    else if (filterChange.type == "delete")
    {
        this.filterList = this.pushFilterSorted(filterChange.value, this.filterList);
    }
  }

  pushFilterSorted(filterText: string, listToChange: string[]): string[] {
    let j = 0;
    let position = this.filterListFull.indexOf(filterText);

    if (position == 0) {
      listToChange.splice(0, 0, filterText);
      return listToChange;
    }

    let notLast = 0;
    for (let i = 0; i < listToChange.length; i++) {
      let currFilter = listToChange[i];
      if (this.filterListFull.indexOf(currFilter) > position) {
        listToChange.splice(i, 0, filterText);
        notLast = 1;
        break;
      }
    }

    if (!notLast) {
      listToChange.push(filterText);
    }

    return listToChange;
  }
  
  generateListOfFilters(): string[] {
    return ["Title", "Case", "Category", "Description", "CreatedBefore", "CreatedAt", "CreatedAfter", "ModifiedBefore", 
            "ModifiedAt", "ModifiedAfter"];
  }

  uploadFile(formData: FormData, options: any): void {
     this.http.post(this._url.documents + "upload/", formData, options)
                  .map((res: any) => res.json())
                  .catch(error => Observable.throw(error))
                  .subscribe(
                      data => console.log('success'),
                      error => console.log(error)
                  );
  }
}