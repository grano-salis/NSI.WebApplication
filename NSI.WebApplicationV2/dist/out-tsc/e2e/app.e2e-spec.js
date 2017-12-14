"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var protractor_1 = require("protractor");
var app_po_1 = require("./app.po");
describe('app', function () {
    var page;
    beforeEach(function () {
        page = new app_po_1.AppPage();
    });
    it('should display login page and login into app', function () {
        page.navigateTo();
        expect(protractor_1.browser.getCurrentUrl()).toContain('/login');
        page.login();
    });
    it('should display hello message', function () {
        page.navigateTo();
        expect(page.getParagraphText()).toEqual('Hello world !');
    });
});
//# sourceMappingURL=app.e2e-spec.js.map