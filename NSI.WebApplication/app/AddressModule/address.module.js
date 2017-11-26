"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var address_component_1 = require("./components/address/address.component");
var AddressModule = (function () {
    function AddressModule() {
    }
    return AddressModule;
}());
AddressModule = __decorate([
    core_1.NgModule({
        imports: [],
        exports: [],
        declarations: [address_component_1.AddressComponent],
        providers: []
    })
], AddressModule);
exports.AddressModule = AddressModule;
//# sourceMappingURL=address.module.js.map