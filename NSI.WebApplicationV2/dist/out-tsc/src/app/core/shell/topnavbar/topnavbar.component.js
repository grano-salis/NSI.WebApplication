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
var TopnavbarComponent = (function () {
    // TypeScript public modifier
    function TopnavbarComponent() {
    }
    TopnavbarComponent.prototype.toggleClicked = function (event) {
        var target = event.srcElement.id;
        var body = $('body');
        var menu = $('#sidebar-menu');
        // toggle small or large menu
        if (body.hasClass('nav-md')) {
            menu.find('li.active ul').hide();
            menu.find('li.active').addClass('active-sm').removeClass('active');
        }
        else {
            menu.find('li.active-sm ul').show();
            menu.find('li.active-sm').addClass('active').removeClass('active-sm');
        }
        body.toggleClass('nav-md nav-sm');
    };
    TopnavbarComponent.prototype.ngOnInit = function () {
        console.log('hello `topnavbar` component');
    };
    TopnavbarComponent = __decorate([
        core_1.Component({
            selector: 'app-topnavbar',
            templateUrl: './topnavbar.component.html',
            styleUrls: ['./topnavbar.component.scss']
        }),
        __metadata("design:paramtypes", [])
    ], TopnavbarComponent);
    return TopnavbarComponent;
}());
exports.TopnavbarComponent = TopnavbarComponent;
//# sourceMappingURL=topnavbar.component.js.map