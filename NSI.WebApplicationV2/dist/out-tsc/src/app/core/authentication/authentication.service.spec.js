"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var authentication_service_1 = require("./authentication.service");
var credentialsKey = 'credentials';
describe('AuthenticationService', function () {
    var authenticationService;
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({
            providers: [authentication_service_1.AuthenticationService]
        });
    });
    beforeEach(testing_1.inject([
        authentication_service_1.AuthenticationService
    ], function (_authenticationService) {
        authenticationService = _authenticationService;
    }));
    afterEach(function () {
        // Cleanup
        localStorage.removeItem(credentialsKey);
        sessionStorage.removeItem(credentialsKey);
    });
    describe('login', function () {
        it('should return credentials', testing_1.fakeAsync(function () {
            // Act
            var request = authenticationService.login({
                username: 'toto',
                password: '123'
            });
            testing_1.tick();
            // Assert
            request.subscribe(function (credentials) {
                expect(credentials).toBeDefined();
                expect(credentials.token).toBeDefined();
            });
        }));
        it('should authenticate user', testing_1.fakeAsync(function () {
            expect(authenticationService.isAuthenticated()).toBe(false);
            // Act
            var request = authenticationService.login({
                username: 'toto',
                password: '123'
            });
            testing_1.tick();
            // Assert
            request.subscribe(function () {
                expect(authenticationService.isAuthenticated()).toBe(true);
                expect(authenticationService.credentials).toBeDefined();
                expect(authenticationService.credentials).not.toBeNull();
                expect(authenticationService.credentials.token).toBeDefined();
                expect(authenticationService.credentials.token).not.toBeNull();
            });
        }));
        it('should persist credentials for the session', testing_1.fakeAsync(function () {
            // Act
            var request = authenticationService.login({
                username: 'toto',
                password: '123'
            });
            testing_1.tick();
            // Assert
            request.subscribe(function () {
                expect(sessionStorage.getItem(credentialsKey)).not.toBeNull();
            });
        }));
        it('should persist credentials across sessions', testing_1.fakeAsync(function () {
            // Act
            var request = authenticationService.login({
                username: 'toto',
                password: '123',
                remember: true
            });
            testing_1.tick();
            // Assert
            request.subscribe(function () {
                expect(localStorage.getItem(credentialsKey)).not.toBeNull();
            });
        }));
    });
    describe('logout', function () {
        it('should clear user authentication', testing_1.fakeAsync(function () {
            // Arrange
            var loginRequest = authenticationService.login({
                username: 'toto',
                password: '123'
            });
            testing_1.tick();
            // Assert
            loginRequest.subscribe(function () {
                expect(authenticationService.isAuthenticated()).toBe(true);
                var request = authenticationService.logout();
                testing_1.tick();
                request.subscribe(function () {
                    expect(authenticationService.isAuthenticated()).toBe(false);
                    expect(authenticationService.credentials).toBeNull();
                    expect(sessionStorage.getItem(credentialsKey)).toBeNull();
                    expect(localStorage.getItem(credentialsKey)).toBeNull();
                });
            });
        }));
        it('should clear persisted user authentication', testing_1.fakeAsync(function () {
            // Arrange
            var loginRequest = authenticationService.login({
                username: 'toto',
                password: '123',
                remember: true
            });
            testing_1.tick();
            // Assert
            loginRequest.subscribe(function () {
                expect(authenticationService.isAuthenticated()).toBe(true);
                var request = authenticationService.logout();
                testing_1.tick();
                request.subscribe(function () {
                    expect(authenticationService.isAuthenticated()).toBe(false);
                    expect(authenticationService.credentials).toBeNull();
                    expect(sessionStorage.getItem(credentialsKey)).toBeNull();
                    expect(localStorage.getItem(credentialsKey)).toBeNull();
                });
            });
        }));
    });
});
//# sourceMappingURL=authentication.service.spec.js.map