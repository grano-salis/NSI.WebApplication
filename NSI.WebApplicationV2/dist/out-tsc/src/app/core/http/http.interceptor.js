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
var http_1 = require("@angular/common/http");
var Observable_1 = require("rxjs/Observable");
require("rxjs/add/operator/do");
require("rxjs/add/operator/catch");
require("rxjs/add/observable/throw");
var ngx_cookie_service_1 = require("ngx-cookie-service");
var NsiHttpInterceptor = (function () {
    function NsiHttpInterceptor(cookieService) {
        this.cookieService = cookieService;
    }
    NsiHttpInterceptor.prototype.intercept = function (request, next) {
        var token = this.cookieService.get('JWT.Token') || 'prazan-token';
        // add a custom header
        var customReq = request.clone({
            headers: request.headers.set('JWTToken', token)
        });
        return next
            .handle(customReq)
            .do(function (ev) {
            if (ev instanceof http_1.HttpResponse) {
                console.log('Evo response', ev);
            }
        })
            .catch(function (response) {
            if (response instanceof http_1.HttpErrorResponse) {
                console.log('Evo error response', response);
                if (response.status === 401) {
                    // redirect to the login route
                    // or show a modal
                }
            }
            return Observable_1.Observable.throw(response);
        });
    };
    NsiHttpInterceptor = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [ngx_cookie_service_1.CookieService])
    ], NsiHttpInterceptor);
    return NsiHttpInterceptor;
}());
exports.NsiHttpInterceptor = NsiHttpInterceptor;
//# sourceMappingURL=http.interceptor.js.map