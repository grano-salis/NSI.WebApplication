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
var addressType_model_1 = require("../addressType.model");
var addressType_service_1 = require("../../../services/addressType.service");
var AddressTypeNewComponent = (function () {
    function AddressTypeNewComponent(addressTypeService) {
        this.addressTypeService = addressTypeService;
        this.date_created = new Date();
        this.date_modified = new Date();
        this.is_deleted = false;
        this.customer_id = 1;
        this.addressType = new addressType_model_1.AddressType();
        this.addressType.createdDate = this.date_created;
        this.addressType.modifiedDate = this.date_modified;
        this.addressType.isDeleted = this.is_deleted;
        this.addressType.customerId = this.customer_id;
    }
    AddressTypeNewComponent.prototype.ngOnInit = function () {
    };
    AddressTypeNewComponent.prototype.onSubmit = function () {
        console.log('Usao');
        console.log(this.addressType);
        console.log('Prosao');
        this.addressTypeService.postAddressType(this.addressType).subscribe(function (r) { return console.log('Post method addressType: ' + r); }, function (error) { return console.log('Error: ' + error.message); });
    };
    AddressTypeNewComponent.prototype.newAddressType = function () {
        this.addressType = new addressType_model_1.AddressType();
    };
    AddressTypeNewComponent = __decorate([
        core_1.Component({
            selector: 'app-address-type-new',
            templateUrl: './address-type-new.component.html',
            styleUrls: ['./address-type-new.component.scss']
        }),
        __metadata("design:paramtypes", [addressType_service_1.AddressTypeService])
    ], AddressTypeNewComponent);
    return AddressTypeNewComponent;
}());
exports.AddressTypeNewComponent = AddressTypeNewComponent;
//# sourceMappingURL=address-type-new.component.js.map