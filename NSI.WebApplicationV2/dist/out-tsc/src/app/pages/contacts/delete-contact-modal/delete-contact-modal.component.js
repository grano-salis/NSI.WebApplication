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
var DeleteContactModalComponent = (function () {
    function DeleteContactModalComponent(contactsService) {
        this.contactsService = contactsService;
        this.deleteFun = new core_1.EventEmitter();
    }
    DeleteContactModalComponent.prototype.deleteContact = function (id) {
        var _this = this;
        this.contactsService.deleteContact(id).subscribe(function (res) {
            _this.deleteFun.emit('emit');
        });
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], DeleteContactModalComponent.prototype, "contact", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", Object)
    ], DeleteContactModalComponent.prototype, "deleteFun", void 0);
    DeleteContactModalComponent = __decorate([
        core_1.Component({
            selector: 'app-delete-contact-modal',
            templateUrl: './delete-contact-modal.component.html',
            styleUrls: ['./delete-contact-modal.component.css']
        }),
        __metadata("design:paramtypes", [contacts_service_1.ContactsService])
    ], DeleteContactModalComponent);
    return DeleteContactModalComponent;
}());
exports.DeleteContactModalComponent = DeleteContactModalComponent;
//# sourceMappingURL=delete-contact-modal.component.js.map