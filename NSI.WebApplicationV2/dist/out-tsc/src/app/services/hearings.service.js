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
var HearingsService = (function () {
    function HearingsService(http) {
        this.http = http;
        this.headers = new http_1.HttpHeaders();
        this._url = environment_1.environment.serverUrl + '/api/hearings';
        this.headers = new http_1.HttpHeaders({ 'Content-Type': 'application/json' });
    }
    HearingsService.prototype.postHearing = function (hearing) {
        var body = JSON.stringify(hearing);
        //let headers = new HttpHeaders({'Content-Type': 'application/json'});
        return this.http.post(this._url, body, { headers: this.headers });
    };
    HearingsService.prototype.putHearing = function (id, hearing) {
        var body = JSON.stringify(hearing);
        //let headers = new HttpHeaders({'Content-Type': 'application/json'});
        return this.http.put(this._url + "/" + id, body, { headers: this.headers });
    };
    HearingsService.prototype.getHearingById = function (id) {
        var params = new http_1.HttpParams();
        params = params.append('id', String(id));
        return this.http.get(this._url + '/' + id); //{headers: this.headers, params: params});
    };
    HearingsService.prototype.deleteHearingById = function (id) {
        return this.http.delete(this._url + "/" + id);
    };
    HearingsService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], HearingsService);
    return HearingsService;
}());
exports.HearingsService = HearingsService;
//# sourceMappingURL=hearings.service.js.map