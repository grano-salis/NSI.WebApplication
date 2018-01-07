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
var router_1 = require("@angular/router");
var comp1_component_1 = require("./comp1/comp1.component");
var comp2_component_1 = require("./comp2/comp2.component");
var example_service_1 = require("./services/example.service");
var exampleRoutes = [
    { path: 'example/comp1', component: comp1_component_1.Comp1Component },
    { path: 'example/comp2/:id', component: comp2_component_1.Comp2Component },
];
var ExampleModule = (function () {
    function ExampleModule() {
    }
    return ExampleModule;
}());
ExampleModule = __decorate([
    core_1.NgModule({
        imports: [common_1.CommonModule, router_1.RouterModule.forChild(exampleRoutes)],
        declarations: [comp1_component_1.Comp1Component, comp2_component_1.Comp2Component],
        providers: [example_service_1.ExampleService],
        exports: [comp1_component_1.Comp1Component, comp2_component_1.Comp2Component]
    })
], ExampleModule);
exports.ExampleModule = ExampleModule;
//# sourceMappingURL=example.module.js.map