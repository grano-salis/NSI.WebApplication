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
var NsiCalendarComponent = (function () {
    function NsiCalendarComponent() {
        this.onCalendarReady = new core_1.EventEmitter();
    }
    NsiCalendarComponent.prototype.ngOnInit = function () {
    };
    NsiCalendarComponent.prototype.ngAfterViewInit = function () {
        var calendar = $(this._selector.nativeElement).fullCalendar(this.nsiFullCalendarConfiguration);
        this.onCalendarReady.emit(calendar);
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], NsiCalendarComponent.prototype, "nsiFullCalendarConfiguration", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", String)
    ], NsiCalendarComponent.prototype, "nsiFullCalendarClass", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", Object)
    ], NsiCalendarComponent.prototype, "onCalendarReady", void 0);
    __decorate([
        core_1.ViewChild('nsiFullCalendar'),
        __metadata("design:type", core_1.ElementRef)
    ], NsiCalendarComponent.prototype, "_selector", void 0);
    NsiCalendarComponent = __decorate([
        core_1.Component({
            selector: 'app-nsi-calendar',
            templateUrl: './nsi-calendar.component.html',
            styleUrls: ['./nsi-calendar.component.scss']
        }),
        __metadata("design:paramtypes", [])
    ], NsiCalendarComponent);
    return NsiCalendarComponent;
}());
exports.NsiCalendarComponent = NsiCalendarComponent;
//# sourceMappingURL=nsi-calendar.component.js.map