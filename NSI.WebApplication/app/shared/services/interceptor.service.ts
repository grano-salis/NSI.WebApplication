import { Injectable } from '@angular/core';
import { Http, ConnectionBackend, RequestOptions, RequestOptionsArgs, Response, Headers, Request } from '@angular/http';
import { PlatformLocation } from '@angular/common';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/finally';
import { Observable }   from 'rxjs/Observable';
declare var jQuery: any;

@Injectable()
export class Interceptor extends Http {
    private logonUrl: string;
    private jwtToken: string;
    private branchID: number = 0;

    constructor(backend: ConnectionBackend, defaultOptions: RequestOptions) {
        super(backend, defaultOptions);
        this.logonUrl = "\\";
        this.jwtToken = jQuery("#app\\.token").val();
    }

    /**
    * Performs a request with `get` http method.
    * @param url
    * @param options
    * @returns {Observable<>}
    */
    get(url: string, options?: RequestOptionsArgs): Observable<Response> {
        return super.get(url, this.requestOptions(options))
            .do((res: Response) => {
            }, (error: any) => {
                this.onError(error);
            })
            .finally(() => {
            });
    }

    /**
    * Performs a request with `post` http method.
    * @param url
    * @param body
    * @param options
    * @returns {Observable<>}
    */
    post(url: string, body: string, options?: RequestOptionsArgs): Observable<Response> {
        return super.post(url, body, this.requestOptions(options))
            .do((res: Response) => {
            }, (error: any) => {
                this.onError(error);
            })
            .finally(() => {
            });
    }

    /**
    * Performs a request with `delete` http method.
    * @param url
    * @param options
    * @returns {Observable<>}
    */
    delete(url: string, options?: RequestOptionsArgs): Observable<Response> {
        return super.delete(url, this.requestOptions(options))
            .do((res: Response) => {
            }, (error: any) => {
                this.onError(error);
            })
            .finally(() => {
            });
    }

    setBranchID(branchID: number) {
        this.branchID = branchID;
    }

    /**
     * Request options.
     * @param options
     * @returns {RequestOptionsArgs}
     */
    private requestOptions(options?: RequestOptionsArgs): RequestOptionsArgs {
        if (options == null) {
            options = new RequestOptions();
        }
        if (options.headers == null) {
            options.headers = new Headers({
                'Content-Type': 'application/json',
                'JwtToken': this.jwtToken,
                'SelectedBranch': this.branchID
            });
        }
        return options;
    }

    /**
     * onError
     * @param error
     */
    private onError(error: any): void {
        if (!!error && error.status == 401) {
            alert("Error");
        }
    }
}