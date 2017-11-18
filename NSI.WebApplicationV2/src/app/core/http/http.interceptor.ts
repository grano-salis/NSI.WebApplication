import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent, HttpResponse, HttpErrorResponse
} from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { CookieService } from 'ngx-cookie-service';
@Injectable()
export class NsiHttpInterceptor implements HttpInterceptor {
  constructor(private cookieService: CookieService) {}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const token = this.cookieService.get('JWT.Token') || 'prazan-token';

    // add a custom header
    const customReq = request.clone({
      headers: request.headers.set('JWTToken', token)
    });

    return next
      .handle(customReq)
      .do((ev: HttpEvent<any>) => {
        if (ev instanceof HttpResponse) {
          console.log('Evo response', ev);
        }
      })
      .catch(response => {
        if (response instanceof HttpErrorResponse) {
          console.log('Evo error response', response);
          if (response.status === 401) {
            // redirect to the login route
            // or show a modal
          }
        }

        return Observable.throw(response);
      });
  }
}
