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
var address_model_1 = require("../address.model");
var address_service_1 = require("../../../services/address.service");
var addressType_service_1 = require("../../../services/addressType.service");
var AddressNewComponent = (function () {
    function AddressNewComponent(addressService, addressTypeService) {
        this.addressService = addressService;
        this.addressTypeService = addressTypeService;
        this.date_created = new Date();
        this.date_modified = new Date();
        this.is_deleted = false;
        // Dio za autocomplete
        this.componentData1 = '';
        this.userSettings = {
            resOnSearchButtonClickOnly: false,
            inputPlaceholderText: 'Start typing address',
            showSearchButton: false,
            showRecentSearch: false,
            showCurrentLocation: false
        };
        this.address = new address_model_1.Address();
        this.address.dateCreated = this.date_created;
        this.address.dateModified = this.date_modified;
        this.address.isDeleted = this.is_deleted;
    }
    AddressNewComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.addressTypeService.getAddressTypes().subscribe(function (addressTypes) {
            _this.addressTypes = addressTypes;
        });
    };
    AddressNewComponent.prototype.onSubmit = function () {
        console.log('Usao');
        console.log(this.address);
        console.log('Prosao');
        this.addressService.postAddress(this.address).subscribe(function (r) { return console.log('Post method address: ' + r); }, function (error) { return console.log('Error: ' + error.message); });
    };
    AddressNewComponent.prototype.newAddress = function () {
        this.address = new address_model_1.Address();
    };
    AddressNewComponent.prototype.autoCompleteCallback1 = function (data) {
        this.componentData1 = JSON.stringify(data);
        if (data.response === true) {
            console.log("Usao");
            this.address.address1 = data.data.address_components[0].long_name;
            this.address.city = String(data.data.address_components[2].long_name);
            this.address.zipCode = +data.data.address_components[6].long_name;
            console.log(this.address.address1);
            console.log(this.address.city);
            console.log(this.address.zipCode);
        }
    };
    AddressNewComponent = __decorate([
        core_1.Component({
            selector: 'app-address-new',
            // template: '<ng4geo-autocomplete (componentCallback)="autoCompleteCallback1($event)"></ng4geo-autocomplete>',
            templateUrl: './address-new.component.html',
            styleUrls: ['./address-new.component.scss']
        }),
        __metadata("design:paramtypes", [address_service_1.AddressService, addressType_service_1.AddressTypeService])
    ], AddressNewComponent);
    return AddressNewComponent;
}());
exports.AddressNewComponent = AddressNewComponent;
//# sourceMappingURL=address-new.component.js.map