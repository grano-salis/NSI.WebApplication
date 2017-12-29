"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var testing_2 = require("@angular/router/testing");
var core_1 = require("@ngx-translate/core");
var app_component_1 = require("./app.component");
var core_module_1 = require("./core/core.module");
describe('AppComponent', function () {
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({
            imports: [
                testing_2.RouterTestingModule,
                core_1.TranslateModule.forRoot(),
                core_module_1.CoreModule
            ],
            declarations: [app_component_1.AppComponent],
            providers: []
        });
        testing_1.TestBed.compileComponents();
    });
    it('should create the app', testing_1.async(function () {
        var fixture = testing_1.TestBed.createComponent(app_component_1.AppComponent);
        var app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));
});
//# sourceMappingURL=app.component.spec.js.map