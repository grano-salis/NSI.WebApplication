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
var Comp2Component = /** @class */ (function () {
    function Comp2Component(route, exampleService) {
        this.route = route;
        this.exampleService = exampleService;
    }
    Comp2Component.prototype.ngOnInit = function () {
        var _this = this;
        this.id = +this.route.snapshot.params['id'];
        this.exampleService.getDataFromRest().subscribe(function (x) { return _this.dataFromRest = x; });
    };
    Comp2Component = __decorate([
        core_1.Component({
            selector: 'comp2-selector',
            template: "\n    <div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\">\n        Other {{id}}\n        <br/>        \n        Some content\n        <br/>\n        Data from REST: {{dataFromRest}}\n    </div>\n    "
        }),
        __metadata("design:paramtypes", [router_1.ActivatedRoute,
            example_service_1.ExampleService])
    ], Comp2Component);
    return Comp2Component;
}());
exports.Comp2Component = Comp2Component;
//# sourceMappingURL=comp2.component.js.map