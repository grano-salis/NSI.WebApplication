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
var logger_service_1 = require("../../core/services/logger.service");
var logger = new logger_service_1.Logger('documents');
var DocumentsComponent = (function () {
    function DocumentsComponent() {
    }
    DocumentsComponent.prototype.ngOnInit = function () {
    };
    DocumentsComponent = __decorate([
        core_1.Component({
            selector: 'app-documents',
            templateUrl: './documents.component.html',
            styleUrls: ['./documents.component.css']
        }),
        __metadata("design:paramtypes", [])
    ], DocumentsComponent);
    return DocumentsComponent;
}());
exports.DocumentsComponent = DocumentsComponent;
//# sourceMappingURL=documents.component.js.map