import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { PagesModule } from './pages/pages.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { QuoteService } from './services/quote.service';
import { CookieService } from 'ngx-cookie-service';
import { TasksService } from './services/tasks.service';
import { NgLoadingSpinnerModule, NgLoadingSpinnerInterceptor } from 'ng-loading-spinner';
import {AlertModule} from 'ngx-bootstrap';
import { HelperService } from './services/helper.service';
import { MeetingsService } from './services/meetings.service';
import { AddressService } from './services/address.service';
import { UsersService } from './services/users.service';
import { ContactsService } from './services/contacts.service';
import { DocumentsService } from './services/documents.service';
import { DocumentsFilterService } from './services/documents-filter.service';
import { HearingsService } from './services/hearings.service';
import { AddressTypeService } from './services/addressType.service';
import { CasesService } from './services/cases.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {PagerService} from "./services/pagination.service";

import { TransactionsService } from './services/transactions.service';
import { PaymentGatewaysService } from './services/payment-gateways.service';
import { PricingPackagesService } from './services/pricing-packages.service';

import { ToastrModule } from 'ngx-toastr';

import {AlertService} from "./services/alert.service";
import { SubscriptionService } from './services/subscription.service';

import { CaseCategoryService } from './services/caseCategory.service';
import { DocumentCategoryService } from './services/document-category.service';
import { FileTypeService } from './services/file-type.service';
import { ClientTypeService } from './services/clientType.service';


const toastrSettings = {
  positionClass: 'toast-top-center',
  timeOut: 3000
};


@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    TranslateModule.forRoot(),
    CoreModule,
    SharedModule,
    PagesModule,
    AppRoutingModule,
    NgLoadingSpinnerModule,
    AlertModule.forRoot(),
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(toastrSettings), // ToastrModule added
  ],
  declarations: [AppComponent],
  providers: [//ovdje se injecta svaki servis koji se doda
    { provide: HTTP_INTERCEPTORS, useClass: NgLoadingSpinnerInterceptor, multi: true },
    CookieService,
    QuoteService,
    TasksService,
    HelperService,
    MeetingsService,
    AddressService,
    AddressTypeService,
    UsersService,
    ContactsService,
    DocumentsService,
    DocumentsFilterService,
    AlertService,
    PaymentGatewaysService,
    PricingPackagesService,
    TransactionsService,
    CasesService,
    HearingsService,
    PagerService,
    SubscriptionService,
    CaseCategoryService,
    ClientTypeService,
    DocumentCategoryService,
    FileTypeService
    //DocumentsService

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
