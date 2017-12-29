"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var platform_browser_1 = require("@angular/platform-browser");
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var core_2 = require("@ngx-translate/core");
var app_component_1 = require("./app.component");
var app_routing_module_1 = require("./app-routing.module");
var core_module_1 = require("./core/core.module");
var shared_module_1 = require("./shared/shared.module");
var pages_module_1 = require("./pages/pages.module");
var http_1 = require("@angular/common/http");
var quote_service_1 = require("./services/quote.service");
var ngx_cookie_service_1 = require("ngx-cookie-service");
var tasks_service_1 = require("./services/tasks.service");
var ng_loading_spinner_1 = require("ng-loading-spinner");
var ngx_bootstrap_1 = require("ngx-bootstrap");
var helper_service_1 = require("./services/helper.service");
var meetings_service_1 = require("./services/meetings.service");
var address_service_1 = require("./services/address.service");
var users_service_1 = require("./services/users.service");
var contacts_service_1 = require("./services/contacts.service");
var documents_service_1 = require("./services/documents.service");
var hearings_service_1 = require("./services/hearings.service");
var addressType_service_1 = require("./services/addressType.service");
var AppModule = (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: [
                platform_browser_1.BrowserModule,
                forms_1.FormsModule,
                http_1.HttpClientModule,
                core_2.TranslateModule.forRoot(),
                core_module_1.CoreModule,
                shared_module_1.SharedModule,
                pages_module_1.PagesModule,
                app_routing_module_1.AppRoutingModule,
                ng_loading_spinner_1.NgLoadingSpinnerModule,
                ngx_bootstrap_1.AlertModule.forRoot()
            ],
            declarations: [app_component_1.AppComponent],
            providers: [
                { provide: http_1.HTTP_INTERCEPTORS, useClass: ng_loading_spinner_1.NgLoadingSpinnerInterceptor, multi: true },
                ngx_cookie_service_1.CookieService,
                quote_service_1.QuoteService,
                tasks_service_1.TasksService,
                helper_service_1.HelperService,
                meetings_service_1.MeetingsService,
                address_service_1.AddressService,
                addressType_service_1.AddressTypeService,
                users_service_1.UsersService,
                contacts_service_1.ContactsService,
                hearings_service_1.HearingsService,
                documents_service_1.DocumentsService
            ],
            bootstrap: [app_component_1.AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map