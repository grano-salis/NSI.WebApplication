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
var contact_1 = require("./contact");
var contacts_service_1 = require("../../../services/contacts.service");
var router_1 = require("@angular/router");
var NewContactComponent = (function () {
    function NewContactComponent(contactsService, route) {
        this.contactsService = contactsService;
        this.route = route;
        this.onClose = new core_1.EventEmitter();
        this.phones = [];
        this.emailsArray = [];
        this.temp_contact = new contact_1.Contact();
    }
    NewContactComponent.prototype.newContact = function () {
        var _this = this;
        this.temp_contact.taskId = 1;
        this.temp_contact.addressId = 1;
        this.temp_contact.createdByUserId = 1;
        this.setPhonesAndEmails();
        this.contactsService.postContact(this.temp_contact).subscribe(function (r) {
            _this.temp_contact.contact1 = r.contact1;
            _this.closeBtn.nativeElement.click();
            _this.onClose.next(_this.temp_contact);
        }, function (error) { return console.log('Error: ', error.message); });
    };
    NewContactComponent.prototype.setPhonesAndEmails = function () {
        this.temp_contact.emails = [{ emailAddress: this.temp_contact.email }];
        var mappedEmails = this.emailsArray.map(function (email) {
            return { emailAddress: email };
        });
        this.temp_contact.emails = this.temp_contact.emailsArray.concat(mappedEmails);
        this.temp_contact.phones = [{ phoneNumber: this.temp_contact.phone }];
        var mappedPhones = this.phones.map(function (phone) {
            return { phoneNumber: phone };
        });
        this.temp_contact.phones = this.temp_contact.phones.concat(mappedPhones);
    };
    NewContactComponent.prototype.newPhone = function () {
        this.phones.push('');
    };
    NewContactComponent.prototype.newEmail = function () {
        this.emailsArray.push('');
    };
    NewContactComponent.prototype.deletePhone = function () {
        this.phones.pop();
    };
    NewContactComponent.prototype.deleteEmail = function () {
        this.emailsArray.pop();
    };
    NewContactComponent.prototype.trackByIndex = function (index, obj) {
        return index;
    };
    __decorate([
        core_1.Output(),
        __metadata("design:type", core_1.EventEmitter)
    ], NewContactComponent.prototype, "onClose", void 0);
    __decorate([
        core_1.ViewChild('closeBtn'),
        __metadata("design:type", core_1.ElementRef)
    ], NewContactComponent.prototype, "closeBtn", void 0);
    NewContactComponent = __decorate([
        core_1.Component({
            selector: 'new-contact-component',
            templateUrl: './new-contact-component.html',
            styleUrls: ['../contacts.component.css']
        }),
        __metadata("design:paramtypes", [contacts_service_1.ContactsService, router_1.ActivatedRoute])
    ], NewContactComponent);
    return NewContactComponent;
}());
exports.NewContactComponent = NewContactComponent;
//# sourceMappingURL=new-contact.component.js.map
