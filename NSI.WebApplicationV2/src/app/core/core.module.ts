import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { ShellComponent } from './shell/shell.component';
import { AuthenticationService } from './authentication/authentication.service';
import { AuthenticationGuard } from './authentication/authentication.guard';
import { I18nService } from './services/i18n.service';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {NsiHttpInterceptor} from "./http/http.interceptor";
import { SidebarComponent } from './shell/sidebar/sidebar.component';
import { TopnavbarComponent } from './shell/topnavbar/topnavbar.component';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    TranslateModule,
    RouterModule
  ],
  declarations: [
    ShellComponent,
    SidebarComponent,
    TopnavbarComponent
  ],
  providers: [
    AuthenticationService,
    AuthenticationGuard,
    I18nService,
    { provide: HTTP_INTERCEPTORS, useClass: NsiHttpInterceptor, multi: true }
  ]
})
export class CoreModule {

  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    // Import guard
    if (parentModule) {
      throw new Error(`${parentModule} has already been loaded. Import Core module in the AppModule only.`);
    }
  }

}
