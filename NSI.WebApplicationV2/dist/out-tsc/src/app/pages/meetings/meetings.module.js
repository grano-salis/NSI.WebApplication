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
var meetings_routing_module_1 = require("./meetings-routing.module");
var meetings_overview_component_1 = require("./meetings-overview/meetings-overview.component");
var shared_module_1 = require("../../shared/shared.module");
var ngx_bootstrap_1 = require("ngx-bootstrap");
var forms_1 = require("@angular/forms");
var meeting_new_component_1 = require("./meeting-new/meeting-new.component");
var MeetingsModule = (function () {
    function MeetingsModule() {
    }
    MeetingsModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule,
                shared_module_1.SharedModule,
                meetings_routing_module_1.MeetingsRoutingModule,
                ngx_bootstrap_1.AlertModule,
                forms_1.FormsModule
            ],
            declarations: [meetings_overview_component_1.MeetingsComponent, meeting_new_component_1.MeetingNewComponent]
        })
    ], MeetingsModule);
    return MeetingsModule;
}());
exports.MeetingsModule = MeetingsModule;
//# sourceMappingURL=meetings.module.js.map