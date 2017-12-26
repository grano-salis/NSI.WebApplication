"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var address_component_1 = require("./address.component");
var i18n_service_1 = require("../../core/services/i18n.service");
var address_new_component_1 = require("./address-new/address-new.component");
var address_list_component_1 = require("./address-list/address-list.component");
var address_type_new_component_1 = require("./address-type-new/address-type-new.component");
var address_type_list_component_1 = require("./address-type-list/address-type-list.component");
var routes = [
    { path: '', component: address_component_1.AddressComponent, data: { title: i18n_service_1.extract('Address') } },
    { path: 'new', component: address_new_component_1.AddressNewComponent },
    { path: 'list', component: address_list_component_1.AddressListComponent },
    { path: 'type/new', component: address_type_new_component_1.AddressTypeNewComponent },
    { path: 'type/list', component: address_type_list_component_1.AddressTypeListComponent }
];
var AddressRoutingModule = (function () {
    function AddressRoutingModule() {
    }
    AddressRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forChild(routes)],
            exports: [router_1.RouterModule]
        })
    ], AddressRoutingModule);
    return AddressRoutingModule;
}());
exports.AddressRoutingModule = AddressRoutingModule;
//# sourceMappingURL=address-routing.module.js.map