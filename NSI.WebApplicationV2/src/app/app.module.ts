import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import {PagesModule} from "./pages/pages.module";
import {HttpClientModule, HTTP_INTERCEPTORS} from "@angular/common/http";
import {QuoteService} from "./services/quote.service";
import { CookieService } from 'ngx-cookie-service';
import { TasksService } from './services/tasks.service';
import { NgLoadingSpinnerModule, NgLoadingSpinnerInterceptor } from 'ng-loading-spinner';
import {AlertModule} from "ngx-bootstrap";
import {HelperService} from "./services/helper.service";
import {ContactsService} from "./services/contacts.service";

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    TranslateModule.forRoot(),
    NgbModule.forRoot(),
    CoreModule,
    SharedModule,
    PagesModule,
    AppRoutingModule,
    NgLoadingSpinnerModule,
    AlertModule.forRoot()
  ],
  declarations: [AppComponent],
  providers: [//ovdje se injecta svaki servis koji se doda
    { provide: HTTP_INTERCEPTORS, useClass: NgLoadingSpinnerInterceptor, multi: true },
    CookieService,
    QuoteService,
    TasksService,
    ContactsService,
    HelperService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
