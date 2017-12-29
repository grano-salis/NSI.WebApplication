import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";

const routes = {
  quote: (c: RandomQuoteContext) => `/jokes/random?category=${c.category}`
};

export interface RandomQuoteContext {
  // The quote's category: 'nerdy', 'explicit'...
  category: string;
}

@Injectable()
export class QuoteService {
  private readonly _url: string;

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl;
  }

  getRandomQuote(context?: RandomQuoteContext): Observable<any> {
    return this.http.get(this._url);
  }

}
