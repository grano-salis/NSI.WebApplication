import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class ContactsService {

  private readonly _url: string;

  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl;
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  getContacts(params?: any): Observable<any> {
    return this.http.get(`${this._url}/api/contacts`);
  }

  editContact(body: any, tempAddress: any): Observable<any> {
    const contact = {
      FirsttName: body.firsttName,
      LastName: body.lastName,
      Phones: body.phones,
      Emails: body.emails,
      Address: tempAddress,
      AddressId: parseInt(body.addressId),
      CreatedByUserId: 6
    };

    return this.http.put(`${this._url}/api/contacts/` + body.contact1, contact);
  }

  deleteContact(params?: number): Observable<any> {
    return this.http.delete(`${this._url}/api/contacts/` + params.toString());
  }

  postContact(contact: any): Observable<any> {
    const body = JSON.stringify(contact);
    const headers = new HttpHeaders({'Content-Type': 'application/json'});
    return this.http.post(`${this._url}/api/contacts/0`, body, {headers: headers});

  }

  getPagedContacts(pageSize: number, pageNumber: number, searchString: string,
                   searchColumn: string, sortOrder: string) {
    let uri = 'pageSize=' + pageSize + '&pageNumber=' + pageNumber;
    if (searchString !== '' || searchColumn !== '') uri += '&searchString=' + searchString;
    if (searchColumn !== '') uri += '&searchColumn=' + searchColumn;
    if (sortOrder !== '') uri += '&sortOrder=' + sortOrder;
    uri += '&caseId=0';
    return this.http.get(`${this._url}/api/contacts?` + uri);
  }
  getContactsByCase(caseId: number): Observable<any> {
		let params = new HttpParams();
		params = params.append('caseId', String(caseId));
		return this.http.get(`${this._url}/forcase/` + caseId);
		//return this.http.get(this._url + 'forcase/' + caseId);  //{headers: this.headers, params: params});    
	  }
}
