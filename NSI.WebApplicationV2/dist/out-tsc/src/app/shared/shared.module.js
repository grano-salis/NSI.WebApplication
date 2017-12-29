"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var loader_component_1 = require("./loader/loader.component");
var comp1_component_1 = require("./comp1/comp1.component");
var comp2_component_1 = require("./comp2/comp2.component");
var nsi_calendar_component_1 = require("./nsi-calendar/nsi-calendar.component");
var SharedModule = (function () {
    function SharedModule() {
    }
    SharedModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule
            ],
            declarations: [
                loader_component_1.LoaderComponent,
                comp1_component_1.Comp1Component,
                comp2_component_1.Comp2Component,
                nsi_calendar_component_1.NsiCalendarComponent
            ],
            exports: [
                loader_component_1.LoaderComponent,
                comp1_component_1.Comp1Component,
                comp2_component_1.Comp2Component,
                nsi_calendar_component_1.NsiCalendarComponent
            ]
        })
    ], SharedModule);
    return SharedModule;
}());
exports.SharedModule = SharedModule;
//# sourceMappingURL=shared.module.js.map