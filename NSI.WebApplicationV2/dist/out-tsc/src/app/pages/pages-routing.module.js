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
var shell_component_1 = require("../core/shell/shell.component");
var routes = [
    // {path: 'login', loadChildren: './login/login.module#LoginModule'}, posto mi koristimo SSO onda nam ne treba ovaj dio
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    {
        path: '',
        component: shell_component_1.ShellComponent,
        children: [
            { path: 'home', loadChildren: './home/home.module#HomeModule' },
            { path: 'about', loadChildren: './about/about.module#AboutModule' },
            { path: 'meetings', loadChildren: './meetings/meetings.module#MeetingsModule' },
            { path: 'organization', loadChildren: './organization/oraganization.module#OrganizationModule' },
            { path: 'contacts', loadChildren: './contacts/contacts.module#ContactsModule' },
            { path: 'documents', loadChildren: './documents/documents.module#DocumentsModule' },
            { path: 'address', loadChildren: './address/address.module#AddressModule' },
            { path: 'addressType', loadChildren: './address/addressType.module#AddressTypeModule' },
            { path: 'hearings', loadChildren: './hearings/hearings.module#HearingsModule' },
        ]
    }
];
var PagesRoutingModule = (function () {
    function PagesRoutingModule() {
    }
    PagesRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forRoot(routes, { useHash: true })],
            exports: [router_1.RouterModule]
        })
    ], PagesRoutingModule);
    return PagesRoutingModule;
}());
exports.PagesRoutingModule = PagesRoutingModule;
//# sourceMappingURL=pages-routing.module.js.map