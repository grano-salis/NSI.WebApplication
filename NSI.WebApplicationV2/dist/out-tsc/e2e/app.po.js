"use strict";
/*
 * Use the Page Object pattern to define the page under test.
 * See docs/coding-guide/e2e-tests.md for more info.
 */
Object.defineProperty(exports, "__esModule", { value: true });
var protractor_1 = require("protractor");
var AppPage = (function () {
    function AppPage() {
        this.usernameField = protractor_1.element(protractor_1.by.css('input[formControlName="username"]'));
        this.passwordField = protractor_1.element(protractor_1.by.css('input[formControlName="password"]'));
        this.loginButton = protractor_1.element(protractor_1.by.css('button[type="submit"]'));
        // Forces default language
        this.navigateTo();
        protractor_1.browser.executeScript(function () { return localStorage.setItem('language', 'en-US'); });
    }
    AppPage.prototype.navigateTo = function () {
        return protractor_1.browser.get('/');
    };
    AppPage.prototype.login = function () {
        this.usernameField.sendKeys('test');
        this.passwordField.sendKeys('123');
        this.loginButton.click();
    };
    AppPage.prototype.getParagraphText = function () {
        return protractor_1.element(protractor_1.by.css('app-root h1')).getText();
    };
    return AppPage;
}());
exports.AppPage = AppPage;
//# sourceMappingURL=app.po.js.map