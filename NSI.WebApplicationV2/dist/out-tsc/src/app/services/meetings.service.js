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
var MeetingsService = (function () {
    function MeetingsService(http) {
        this.http = http;
        this.headers = new http_1.HttpHeaders();
        this._url = environment_1.environment.serverUrl + '/api/meetings';
        this.headers = new http_1.HttpHeaders({ 'Content-Type': 'application/json' });
    }
    MeetingsService.prototype.postMeeting = function (meeting) {
        var body = JSON.stringify(meeting);
        var headers = new http_1.HttpHeaders({ 'Content-Type': 'application/json' });
        return this.http.post(this._url, body, { headers: headers });
    };
    MeetingsService.prototype.putMeeting = function (id, meeting) {
        var body = JSON.stringify(meeting);
        var headers = new http_1.HttpHeaders({ 'Content-Type': 'application/json' });
        return this.http.put(this._url + "/" + id, body, { headers: headers });
    };
    MeetingsService.prototype.getMeetings = function (params) {
        return this.http.get(this._url);
    };
    MeetingsService.prototype.getMeetingById = function (id) {
        return this.http.get(this._url + "/" + id);
    };
    MeetingsService.prototype.deleteMeetingById = function (id) {
        return this.http.delete(this._url + "/" + id);
    };
    MeetingsService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], MeetingsService);
    return MeetingsService;
}());
exports.MeetingsService = MeetingsService;
//# sourceMappingURL=meetings.service.js.map