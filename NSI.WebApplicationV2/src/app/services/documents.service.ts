import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders, HttpRequest, HttpParams } from '@angular/common/http';
import { RequestOptions, Headers } from "@angular/http";
import { Observable } from 'rxjs/Observable';
import { Observer } from 'rxjs/Observer';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/map';

import { CaseDetail } from '../pages/cases/case-detail/caseDetail.model';

import {
  Document,
  DocumentDetails,
  DocumentQuery,
  DocumentFilter,
  DocumentCategory,
  Item
} from '../pages/documents/models/index.model';

import { MDD } from '../pages/documents/models/mockDocumentDetails';

@Injectable()
export class DocumentsService {
  filterList: string[];

  private readonly filterListFull: string[];    
  private readonly _url: any;
  private headers = new HttpHeaders();

  documentAdded = new Subject<DocumentDetails>();
  documentUpdated = new Subject<DocumentDetails>();
  documentUpdatingRequested = new Subject();
  documentCaseChanged = new Subject<number>();
  documentHistoryRequested = new Subject<DocumentDetails>();
  documentAll = new Subject();
  
  newFilterEvent = new Subject();
  chosenFilterEvent = new Subject<DocumentFilter>();
  updateFilter = new Subject<DocumentFilter>();
  submitFiltering = new Subject();
  
  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
    this._url = {
      'documents': environment.serverUrl + '/api/documents/',
      'cases': environment.serverUrl + '/api/case/info',
      'categories': environment.serverUrl + '/api/categories/'
      };
    
    this.filterListFull = this.generateListOfFilters();
    this.filterList = this.generateListOfFilters();
    
    this.subscribe();
  }

  getDocuments(): Observable<DocumentDetails[]> {
    return this.http.get<DocumentDetails[]>(this._url.documents, {headers: this.headers});
  }

  getDocumentsWithPaging(queryModel: DocumentQuery): Observable<any> {
    return this.http.post(this._url.documents + 'paging', queryModel, {headers: this.headers});
  }

  getDocumentHistoryByDocumentId(docId: number): Observable<any> {
    return this.http.get(this._url.documents + 'history/' + docId, {headers: this.headers});
  }

  getDocumentById(documentId: number): Observable<any> {
    return this.http.get(this._url.documents + documentId, {headers: this.headers});    
  }
  
  postDocument(document: Document): Observable<any> {
    let body = JSON.stringify(document);
    return this.http.post(this._url.documents, body, {headers: this.headers});
  }

  putDocument(document: Document): Observable<any> {
    let body = JSON.stringify(document);
    return this.http.put(this._url.documents + document.documentId, body, {headers: this.headers});
  }

  deleteDocument(documentId: number): Observable<any> {
    return this.http.delete(this._url.documents + documentId, {headers: this.headers});
  }

  getCaseList(): Observable<Item[]> {
    return this.http.get<CaseDetail[]>(this._url.cases, {headers: this.headers})
      .map((cases: CaseDetail[]) => {
        let caseItems: Item[] = [];

        for (let index in cases) {
          caseItems.push(this.mapCaseToItem(cases[index]));
        }

        return caseItems;
      });
  }

  getCategoryList(): Observable<Item[]> {
    return this.http.get<DocumentCategory[]>(this._url.documents + 'category', {headers: this.headers})
      .map((categories: DocumentCategory[]) => {
        let categoryItems: Item[] = [];

        for (let index in categories) {
          categoryItems.push(this.mapCategoryToItem(categories[index]));
        }

        return categoryItems;
      });
  }

  mapCaseToItem(caseDetail: CaseDetail): Item {
    return new Item(
      caseDetail.caseId,
      caseDetail.caseNumber
    );
  }

  mapCategoryToItem(documentCategory: DocumentCategory): Item {
    return new Item(
      documentCategory.id,
      documentCategory.name
    );
  }

  onFilterChangeDetected(filterChange: DocumentFilter): void {
    if (filterChange.type == "default") {
      this.filterList.splice(0, 1);
    }
    else if (filterChange.type == "add") 
    {
      for (let i = this.filterList.length - 1; i >= 0; i--) {
        if (this.filterList[i] === filterChange.field) {
          this.filterList.splice(i, 1);
          break;
        }
      }
    }
    else if (filterChange.type == "delete")
    {
        this.filterList = this.pushFilterSorted(filterChange.field, this.filterList);
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
    return ["Title", "Case", "Category", "Description", "CreatedBefore", "CreatedAfter", "ModifiedBefore", "ModifiedAfter"];
  }

  uploadFile(formData: FormData): Observable<any> {
    let headers = new HttpHeaders({'Accept': 'application/json'});
    return this.http.post(this._url.documents + "upload", formData, {headers: headers})
              .map((path: string) => { return path; });
  }

  getNumberOfDocumentsByCase(caseId: number): Observable<any> {
    let params = new HttpParams();
    params = params.append('caseId', String(caseId));
    return this.http.get(this._url.documents + 'case/' + caseId);  //{headers: this.headers, params: params});    
  }

  getDocumentsByCase(caseId: number): Observable<any> {
    let params = new HttpParams();
    params = params.append('caseId', String(caseId));
    return this.http.get(this._url.documents + 'byCase/' + caseId);  //{headers: this.headers, params: params});    
  }

  subscribe() {
    this.chosenFilterEvent.subscribe((documentFilter: DocumentFilter) => this.onFilterChangeDetected(documentFilter));
  }
}