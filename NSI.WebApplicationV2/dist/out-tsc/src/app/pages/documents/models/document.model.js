"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Document = (function () {
    function Document(documentId, caseId, documentCategoryId, fileTypeId, documentPath, documentContent, createdByUserId) {
        this.documentId = documentId;
        this.caseId = caseId;
        this.documentCategoryId = documentCategoryId;
        this.fileTypeId = fileTypeId;
        this.documentPath = documentPath;
        this.documentContent = documentContent;
        this.createdByUserId = createdByUserId;
    }
    return Document;
}());
exports.Document = Document;
//# sourceMappingURL=document.model.js.map