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
var contacts_service_1 = require("../../services/contacts.service");
var Contact = (function () {
    function Contact() {
        this.firsttName = '';
        this.lastName = '';
        this.phone = '';
        this.email = '';
        this.mobile = '';
    }
    return Contact;
}());
var ContactsComponent = (function () {
    function ContactsComponent(contactsService) {
        this.contactsService = contactsService;
        var _this = this;
        this.filterColumn = 'name';
        this.filterValue = '';
        setTimeout(function () {
            $(function () {
                _this.initTable();
            });
        }, 700);
        this.temp_contact = new Contact();
    }
    ContactsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.contactsService.getContacts().subscribe(function (contacts) {
            _this.allContacts = contacts;
            _this.contacts = contacts;
        });
    };
    ContactsComponent.prototype.addContact = function () {
        console.log('add contact');
    };
    ContactsComponent.prototype.editContact = function (contact) {
        this.temp_contact = Object.assign({}, contact);
    };
    ContactsComponent.prototype.newContact = function () {
        // this.temp_contact = new Contact();
    };
    ContactsComponent.prototype.showContact = function (contact) {
        this.temp_contact = Object.assign({}, contact);
    };
    ContactsComponent.prototype.close = function () {
        var _this = this;
        this.allContacts[this.allContacts.findIndex(function (c) { return c.contact1 === _this.temp_contact.contact1; })] =
            this.temp_contact;
        this.search();
    };
    ContactsComponent.prototype.DeleteElement = function (contactToDelete) {
        var index = this.allContacts.findIndex(function (c) { return c.contact1 === contactToDelete.contact1; });
        this.allContacts.splice(index, 1);
        this.search();
    };
    ContactsComponent.prototype.AddingNewContactCallback = function (newContact) {
        this.allContacts.unshift(newContact);
        this.search();
    };
    ContactsComponent.prototype.initTable = function () {
        $('#datatable').dataTable({
            'searching': false,
            'bAutoWidth': false,
            'bLengthChange': false,
            'aoColumns': [
                { 'bSortable': true },
                { 'bSortable': true },
                { 'bSortable': true },
                { 'bSortable': false },
                { 'bSortable': false },
                { 'bSortable': false }
            ]
        });
    };
    ContactsComponent.prototype.search = function () {
        var _this = this;
        var __this = this;
        var filterValue = this.filterValue.toLocaleLowerCase();
        this.contacts = this.allContacts.filter(function (contact) {
            if (_this.filterColumn === 'name') {
                return contact.firsttName.toLocaleLowerCase().includes(filterValue) ||
                    contact.lastName.toLocaleLowerCase().includes(filterValue) ||
                    (contact.lastName + ' ' + contact.firsttName).toLocaleLowerCase().includes(filterValue) ||
                    (contact.firsttName + ' ' + contact.lastName).toLocaleLowerCase().includes(filterValue);
            }
            return contact[_this.filterColumn].toLocaleLowerCase().includes(filterValue);
        });
        $('#datatable').dataTable().fnDestroy();
        setTimeout(function () {
            $(function () {
                __this.initTable();
            });
        }, 100);
    };
    ContactsComponent.prototype.changeFilterColumn = function () {
        var __this = this;
        this.filterValue = '';
        this.contacts = this.allContacts;
        $('#datatable').dataTable().fnDestroy();
        setTimeout(function () {
            $(function () {
                __this.initTable();
            });
        }, 100);
    };
    ContactsComponent = __decorate([
        core_1.Component({
            selector: 'app-contacts',
            templateUrl: './contacts.component.html',
            styleUrls: ['./contacts.component.css']
        }),
        __metadata("design:paramtypes", [contacts_service_1.ContactsService])
    ], ContactsComponent);
    return ContactsComponent;
}());
exports.ContactsComponent = ContactsComponent;
//# sourceMappingURL=contacts.component.js.map