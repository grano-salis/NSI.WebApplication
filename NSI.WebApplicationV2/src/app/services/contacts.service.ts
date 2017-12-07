import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class ContactsService {

  private readonly _url: string;

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl;
  }

  getContacts(params?: any): Observable<any> {
    return this.http.get(`${this._url}/api/contacts`);
  }

  editContact(body: any): Observable<any> {
    const contact = {
      FirsttName: body.firsttName,
      LastName: body.lastName,
      Phone: body.phone,
      Email: body.email,
      Mobile: body.mobile,
      AddresId: 1,
      CreatedByUserId: 6
    };

    return this.http.put(`${this._url}/api/contacts/` + body.contact1, contact);
  }

}
