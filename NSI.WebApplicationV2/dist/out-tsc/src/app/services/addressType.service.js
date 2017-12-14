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
var environment_1 = require("../../environments/environment");
var http_1 = require("@angular/common/http");
var AddressTypeService = (function () {
    function AddressTypeService(http) {
        this.http = http;
        this.headers = new http_1.HttpHeaders();
        this._url = environment_1.environment.serverUrl + '/api/addresstype';
        this.headers = new http_1.HttpHeaders({ 'Content-Type': 'application/json' });
    }
    AddressTypeService.prototype.getAddressTypes = function (params) {
        return this.http.get("" + this._url);
    };
    AddressTypeService.prototype.postAddressType = function (addressType) {
        var body = JSON.stringify(addressType);
        return this.http.post(environment_1.environment.serverUrl + '/api/addresstype', body, { headers: this.headers });
    };
    AddressTypeService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], AddressTypeService);
    return AddressTypeService;
}());
exports.AddressTypeService = AddressTypeService;
//# sourceMappingURL=addressType.service.js.map