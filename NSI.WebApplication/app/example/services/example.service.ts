import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Interceptor } from '../../shared/services/interceptor.service';
import 'rxjs/add/operator/map';
import { Observable } from "rxjs/Observable";

@Injectable()
export class ExampleService {
    private readonly REST_URL = "http://localhost:59737/";

    private urls = {
        getRESTData: this.REST_URL + "api/Test",
        postData: this.REST_URL + "api/Test/postURL",
    };

    constructor(private http: Interceptor) { }
    
    getDataFromRest(): Observable<string> {
        return this.http.get(this.urls.getRESTData)
            .map(x => x.json());
    }

    postDataToRest(id: number, text: string): Observable<string> {
        return this.http.post(this.urls.postData, JSON.stringify({a: id, b: text}))
            .map(x => x.json());
    }    
}