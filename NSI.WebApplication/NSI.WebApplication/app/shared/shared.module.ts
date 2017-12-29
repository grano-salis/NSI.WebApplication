import { NgModule }                         from '@angular/core';
import { Http, Request, RequestOptionsArgs, Response, XHRBackend, RequestOptions, ConnectionBackend, Headers, HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule }                     from '@angular/router';
import { PlatformLocation }                 from '@angular/common';
import { CommonModule }                     from '@angular/common';
import { BrowserModule }                    from '@angular/platform-browser';

import { ValidationService }        from './services/validation.service';
import { Interceptor }          from './services/interceptor.service';

@NgModule({
    imports: [BrowserModule, CommonModule, FormsModule, ReactiveFormsModule, HttpModule, RouterModule],
    declarations: [],
    providers: [ValidationService, {
        provide: Interceptor,
        useFactory:
        (backend: XHRBackend, defaultOptions: RequestOptions) => {
            return new Interceptor(backend, defaultOptions);
        },
        deps: [XHRBackend, RequestOptions]
    }],
    exports: [BrowserModule, ReactiveFormsModule, FormsModule, CommonModule, HttpModule, CommonModule, RouterModule]
})
export class SharedModule { }