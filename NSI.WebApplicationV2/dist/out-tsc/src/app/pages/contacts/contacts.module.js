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
var contact_modal_component_1 = require("./contact-modal/contact-modal.component");
var contacts_routing_module_1 = require("./contacts-routing.module");
var contacts_component_1 = require("./contacts.component");
var shared_module_1 = require("../../shared/shared.module");
var new_contact_component_1 = require("./new-contact/new-contact.component");
var ngx_bootstrap_1 = require("ngx-bootstrap");
var forms_1 = require("@angular/forms");
var delete_contact_modal_component_1 = require("./delete-contact-modal/delete-contact-modal.component");
var show_contact_component_1 = require("./show-contact-modal/show-contact.component");
var ContactsModule = (function () {
    function ContactsModule() {
    }
    ContactsModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule,
                shared_module_1.SharedModule,
                contacts_routing_module_1.ContactsRoutingModule,
                forms_1.FormsModule,
                ngx_bootstrap_1.AlertModule,
            ],
            declarations: [
                contacts_component_1.ContactsComponent,
                contact_modal_component_1.ContactModalComponent,
                delete_contact_modal_component_1.DeleteContactModalComponent,
                new_contact_component_1.NewContactComponent,
                show_contact_component_1.ShowContactComponent
            ]
        })
    ], ContactsModule);
    return ContactsModule;
}());
exports.ContactsModule = ContactsModule;
//# sourceMappingURL=contacts.module.js.map