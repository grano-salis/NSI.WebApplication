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
var ContactsService = (function () {
    function ContactsService(http) {
        this.http = http;
        this.headers = new http_1.HttpHeaders();
        this._url = environment_1.environment.serverUrl;
        this.headers = new http_1.HttpHeaders({ 'Content-Type': 'application/json' });
    }
    ContactsService.prototype.getContacts = function (params) {
        return this.http.get(this._url + "/api/contacts");
    };
    ContactsService.prototype.editContact = function (body) {
        var contact = {
            FirsttName: body.firsttName,
            LastName: body.lastName,
            Phone: body.phone,
            Email: body.email,
            Mobile: body.mobile,
            AddresId: 1,
            CreatedByUserId: 6
        };
        return this.http.put(this._url + "/api/contacts/" + body.contact1, contact);
    };
    ContactsService.prototype.deleteContact = function (params) {
        return this.http.delete(this._url + "/api/contacts/" + params.toString());
    };
    ContactsService.prototype.postContact = function (contact) {
        var body = JSON.stringify(contact);
        var headers = new http_1.HttpHeaders({ 'Content-Type': 'application/json' });
        console.log(body);
        return this.http.post(this._url + "/api/contacts", body, { headers: headers });
    };
    ContactsService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], ContactsService);
    return ContactsService;
}());
exports.ContactsService = ContactsService;
//# sourceMappingURL=contacts.service.js.map