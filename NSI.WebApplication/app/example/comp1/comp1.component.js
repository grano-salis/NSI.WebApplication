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
var example_service_1 = require("../services/example.service");
var Comp1Component = /** @class */ (function () {
    function Comp1Component(route, exampleService) {
        this.route = route;
        this.exampleService = exampleService;
    }
    Comp1Component.prototype.ngOnInit = function () {
        var _this = this;
        this.id = 150;
        this.exampleService.postDataToRest(this.id, "it could be from form").subscribe(function (x) { return _this.dataFromRest = x; });
    };
    Comp1Component = __decorate([
        core_1.Component({
            selector: 'comp2-selector',
            template: "\n    <div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\">\n        F Component {{id}}\n        <br/>        \n        First Component\n        <br/>\n        Data from REST: {{dataFromRest}}\n    </div>\n    "
        }),
        __metadata("design:paramtypes", [router_1.ActivatedRoute,
            example_service_1.ExampleService])
    ], Comp1Component);
    return Comp1Component;
}());
exports.Comp1Component = Comp1Component;
//# sourceMappingURL=comp1.component.js.map