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
var DocumentsService = (function () {
    function DocumentsService(http) {
        this.http = http;
        this.headers = new http_1.HttpHeaders();
        this._url = environment_1.environment.serverUrl + '/api/documents/';
        this.headers = new http_1.HttpHeaders({ 'Content-Type': 'application/json' });
    }
    DocumentsService.prototype.getDocuments = function () {
        // return Observable.create( observer => {
        //   observer.next(MDD);
        //   observer.complete();
        // });
        return this.http.get(this._url, { headers: this.headers });
    };
    DocumentsService.prototype.getDocumentsWithPaging = function (queryModel) {
        var body = JSON.stringify(queryModel);
        return this.http.post(this._url, body, { headers: this.headers });
    };
    DocumentsService.prototype.getDocumentById = function (documentId) {
        return this.http.get(this._url + documentId, { headers: this.headers });
    };
    DocumentsService.prototype.postDocument = function (document) {
        var body = JSON.stringify(document);
        return this.http.post(this._url, body, { headers: this.headers });
    };
    DocumentsService.prototype.putDocument = function (document) {
        var body = JSON.stringify(document);
        return this.http.post(this._url, body, { headers: this.headers });
    };
    DocumentsService.prototype.deleteDocument = function (document) {
        var body = JSON.stringify(document);
        return this.http.post(this._url, body, { headers: this.headers });
    };
    DocumentsService.prototype.uploadFile = function (formData) {
        var body = JSON.stringify(document);
        return this.http.post(this._url, body, { headers: this.headers });
    };
    
    DocumentsService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], DocumentsService);
    return DocumentsService;
}());
exports.DocumentsService = DocumentsService;
//# sourceMappingURL=documents.service.js.map