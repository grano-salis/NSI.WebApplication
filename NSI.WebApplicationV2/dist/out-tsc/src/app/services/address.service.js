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
var AddressService = (function () {
    function AddressService(http) {
        this.http = http;
        this.headers = new http_1.HttpHeaders();
        this._url = environment_1.environment.serverUrl + '/api/address';
        this.headers = new http_1.HttpHeaders({ 'Content-Type': 'application/json' });
    }
    AddressService.prototype.getAddreses = function (params) {
        return this.http.get("" + this._url);
    };
    AddressService.prototype.postAddress = function (address) {
        var body = JSON.stringify(address);
        return this.http.post(environment_1.environment.serverUrl + '/api/address', body, { headers: this.headers });
    };
    AddressService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], AddressService);
    return AddressService;
}());
exports.AddressService = AddressService;
//# sourceMappingURL=address.service.js.map