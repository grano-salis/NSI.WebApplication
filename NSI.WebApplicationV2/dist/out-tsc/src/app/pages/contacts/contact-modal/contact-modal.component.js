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
var contacts_service_1 = require("../../../services/contacts.service");
var ContactModalComponent = (function () {
    function ContactModalComponent(contactsService) {
        this.contactsService = contactsService;
        this.onClose = new core_1.EventEmitter();
    }
    ContactModalComponent.prototype.ngAfterViewInit = function () {
    };
    ContactModalComponent.prototype.ngOnInit = function () {
    };
    ContactModalComponent.prototype.updateContact = function () {
        var _this = this;
        this.contactsService.editContact(this.temp_contact).subscribe(function (contact) {
            _this.closeBtn.nativeElement.click();
            _this.onClose.next(_this.temp_contact); // emit event
        });
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], ContactModalComponent.prototype, "temp_contact", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", core_1.EventEmitter)
    ], ContactModalComponent.prototype, "onClose", void 0);
    __decorate([
        core_1.ViewChild('closeBtn'),
        __metadata("design:type", core_1.ElementRef)
    ], ContactModalComponent.prototype, "closeBtn", void 0);
    ContactModalComponent = __decorate([
        core_1.Component({
            selector: 'app-contact-modal',
            templateUrl: './contact-modal.component.html',
            styleUrls: ['../contacts.component.css']
        }),
        __metadata("design:paramtypes", [contacts_service_1.ContactsService])
    ], ContactModalComponent);
    return ContactModalComponent;
}());
exports.ContactModalComponent = ContactModalComponent;
//# sourceMappingURL=contact-modal.component.js.map