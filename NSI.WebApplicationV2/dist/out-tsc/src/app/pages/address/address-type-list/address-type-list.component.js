"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var addressType_service_1 = require("../../../services/addressType.service");
var AddressTypeListComponent = (function () {
    function AddressTypeListComponent(addressTypeService) {
        this.addressTypeService = addressTypeService;
    }
    AddressTypeListComponent.prototype.ngOnInit = function () {
        this.loadAddressTypes();
    };
    AddressTypeListComponent.prototype.loadAddressTypes = function () {
        var _this = this;
        this.addressTypeService.getAddressTypes().subscribe(function (addressTypes) {
            _this.addressTypes = addressTypes;
        });
    };
    AddressTypeListComponent = __decorate([
        core_1.Component({
            selector: 'app-address-type-list',
            templateUrl: './address-type-list.component.html',
            styleUrls: ['./address-type-list.component.scss']
        }),
        __metadata("design:paramtypes", [addressType_service_1.AddressTypeService])
    ], AddressTypeListComponent);
    return AddressTypeListComponent;
}());
exports.AddressTypeListComponent = AddressTypeListComponent;
//# sourceMappingURL=address-type-list.component.js.map