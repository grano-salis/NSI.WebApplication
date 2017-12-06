"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var CustomersDetailsComponent_1 = require("./CustomerDetails/CustomersDetailsComponent");
var customersListComponent_1 = require("./CustomersList/customersListComponent");
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var forms_1 = require("@angular/forms");
var router_1 = require("@angular/router");
var OrganizationModule = (function () {
    function OrganizationModule() {
    }
    return OrganizationModule;
}());
OrganizationModule = __decorate([
    core_1.NgModule({
        imports: [/*CoreModule.forRoot(),*/ platform_browser_1.BrowserModule, forms_1.FormsModule, router_1.RouterModule.forRoot([])],
        declarations: [customersListComponent_1.CustomersListComponent, CustomersDetailsComponent_1.CustomersDetailsComponent],
        bootstrap: [customersListComponent_1.CustomersListComponent, CustomersDetailsComponent_1.CustomersDetailsComponent]
    })
], OrganizationModule);
exports.OrganizationModule = OrganizationModule;
//# sourceMappingURL=oraganization.module.js.map