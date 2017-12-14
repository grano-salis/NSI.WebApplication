"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Observable_1 = require("rxjs/Observable");
var MockAuthenticationService = (function () {
    function MockAuthenticationService() {
        this.credentials = {
            username: 'test',
            token: '123'
        };
    }
    MockAuthenticationService.prototype.login = function (context) {
        return Observable_1.Observable.of({
            username: context.username,
            token: '123456'
        });
    };
    MockAuthenticationService.prototype.logout = function () {
        this.credentials = null;
        return Observable_1.Observable.of(true);
    };
    MockAuthenticationService.prototype.isAuthenticated = function () {
        return !!this.credentials;
    };
    return MockAuthenticationService;
}());
exports.MockAuthenticationService = MockAuthenticationService;
//# sourceMappingURL=authentication.service.mock.js.map