"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var forms_1 = require("@angular/forms");
var router_1 = require("@angular/router");
var common_1 = require("@angular/common");
var platform_browser_1 = require("@angular/platform-browser");
var validation_service_1 = require("./services/validation.service");
var interceptor_service_1 = require("./services/interceptor.service");
var SharedModule = /** @class */ (function () {
    function SharedModule() {
    }
    SharedModule = __decorate([
        core_1.NgModule({
            imports: [platform_browser_1.BrowserModule, common_1.CommonModule, forms_1.FormsModule, forms_1.ReactiveFormsModule, http_1.HttpModule, router_1.RouterModule],
            declarations: [],
            providers: [validation_service_1.ValidationService, {
                    provide: interceptor_service_1.Interceptor,
                    useFactory: function (backend, defaultOptions) {
                        return new interceptor_service_1.Interceptor(backend, defaultOptions);
                    },
                    deps: [http_1.XHRBackend, http_1.RequestOptions]
                }],
            exports: [platform_browser_1.BrowserModule, forms_1.ReactiveFormsModule, forms_1.FormsModule, common_1.CommonModule, http_1.HttpModule, common_1.CommonModule, router_1.RouterModule]
        })
    ], SharedModule);
    return SharedModule;
}());
exports.SharedModule = SharedModule;
//# sourceMappingURL=shared.module.js.map