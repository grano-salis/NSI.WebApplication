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
var documents_routing_module_1 = require("./documents-routing.module");
var documents_component_1 = require("./documents.component");
var document_new_component_1 = require("./document-new/document-new.component");
var document_modal_component_1 = require("./document-modal/document-modal.component");
var document_list_component_1 = require("./document-list/document-list.component");
var shared_module_1 = require("../../shared/shared.module");
var DocumentsModule = (function () {
    function DocumentsModule() {
    }
    DocumentsModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule,
                shared_module_1.SharedModule,
                documents_routing_module_1.DocumentsRoutingModule
            ],
            declarations: [
                documents_component_1.DocumentsComponent,
                document_new_component_1.DocumentNewComponent,
                document_modal_component_1.DocumentModalComponent,
                document_list_component_1.DocumentListComponent
            ]
        })
    ], DocumentsModule);
    return DocumentsModule;
}());
exports.DocumentsModule = DocumentsModule;
//# sourceMappingURL=documents.module.js.map