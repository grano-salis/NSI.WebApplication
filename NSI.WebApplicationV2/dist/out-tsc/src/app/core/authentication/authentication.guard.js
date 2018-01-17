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
var router_1 = require("@angular/router");
var logger_service_1 = require("../services/logger.service");
var authentication_service_1 = require("./authentication.service");
var log = new logger_service_1.Logger('AuthenticationGuard');
var AuthenticationGuard = (function () {
    function AuthenticationGuard(router, authenticationService) {
        this.router = router;
        this.authenticationService = authenticationService;
    }
    AuthenticationGuard.prototype.canActivate = function () {
        if (this.authenticationService.isAuthenticated()) {
            return true;
        }
        //log.debug('Not authenticated, redirecting...');
        //window.location = url od SSO
        //this.router.navigate(['/login'], { replaceUrl: true });
        return true; //ovo treba staviti na false
    };
    AuthenticationGuard = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [router_1.Router,
            authentication_service_1.AuthenticationService])
    ], AuthenticationGuard);
    return AuthenticationGuard;
}());
exports.AuthenticationGuard = AuthenticationGuard;
//# sourceMappingURL=authentication.guard.js.map