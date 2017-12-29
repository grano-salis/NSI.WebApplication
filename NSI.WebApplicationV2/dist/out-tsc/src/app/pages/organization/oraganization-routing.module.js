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
var router_1 = require("@angular/router");
var i18n_service_1 = require("../../core/services/i18n.service");
var routes = [
    { path: '', component: customersListComponent_1.CustomersListComponent, data: { title: i18n_service_1.extract('Customers') } },
    { path: 'name', component: CustomersDetailsComponent_1.CustomersDetailsComponent, data: { title: i18n_service_1.extract('Name') } }
];
var OrganizationRoutingModule = (function () {
    function OrganizationRoutingModule() {
    }
    OrganizationRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forChild(routes)],
            exports: [router_1.RouterModule]
        })
    ], OrganizationRoutingModule);
    return OrganizationRoutingModule;
}());
exports.OrganizationRoutingModule = OrganizationRoutingModule;
//# sourceMappingURL=oraganization-routing.module.js.map