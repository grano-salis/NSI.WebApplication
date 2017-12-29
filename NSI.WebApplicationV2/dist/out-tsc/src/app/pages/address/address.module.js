"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var address_routing_module_1 = require("./address-routing.module");
var address_component_1 = require("./address.component");
var shared_module_1 = require("../../shared/shared.module");
var address_new_component_1 = require("./address-new/address-new.component");
var address_list_component_1 = require("./address-list/address-list.component");
var forms_1 = require("@angular/forms");
var address_type_list_component_1 = require("./address-type-list/address-type-list.component");
var address_type_new_component_1 = require("./address-type-new/address-type-new.component");
var ng4_geoautocomplete_1 = require("ng4-geoautocomplete");
var AddressModule = (function () {
    function AddressModule() {
    }
    AddressModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule,
                shared_module_1.SharedModule,
                address_routing_module_1.AddressRoutingModule,
                forms_1.FormsModule,
                ng4_geoautocomplete_1.Ng4GeoautocompleteModule.forRoot()
            ],
            declarations: [address_component_1.AddressComponent, address_new_component_1.AddressNewComponent, address_list_component_1.AddressListComponent, address_type_new_component_1.AddressTypeNewComponent, address_type_list_component_1.AddressTypeListComponent]
        })
    ], AddressModule);
    return AddressModule;
}());
exports.AddressModule = AddressModule;
//# sourceMappingURL=address.module.js.map