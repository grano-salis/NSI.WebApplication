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
import {HttpClientModule} from "@angular/common/http";
import {QuoteService} from "./services/quote.service";
import { CookieService } from 'ngx-cookie-service';
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
    AppRoutingModule
  ],
  declarations: [AppComponent],
  providers: [//ovdje se injecta svaki servis koji se doda
    CookieService,
    QuoteService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
