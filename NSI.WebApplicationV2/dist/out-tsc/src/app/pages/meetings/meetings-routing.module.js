"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var meetings_overview_component_1 = require("./meetings-overview/meetings-overview.component");
var i18n_service_1 = require("../../core/services/i18n.service");
var meeting_new_component_1 = require("./meeting-new/meeting-new.component");
var routes = [
    { path: '', component: meetings_overview_component_1.MeetingsComponent, data: { title: i18n_service_1.extract('Meetings') } },
    { path: 'new', component: meeting_new_component_1.MeetingNewComponent },
    { path: 'edit/:id', component: meeting_new_component_1.MeetingNewComponent },
    { path: 'delete/:id', component: meeting_new_component_1.MeetingNewComponent }
];
var MeetingsRoutingModule = (function () {
    function MeetingsRoutingModule() {
    }
    MeetingsRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forChild(routes)],
            exports: [router_1.RouterModule]
        })
    ], MeetingsRoutingModule);
    return MeetingsRoutingModule;
}());
exports.MeetingsRoutingModule = MeetingsRoutingModule;
//# sourceMappingURL=meetings-routing.module.js.map