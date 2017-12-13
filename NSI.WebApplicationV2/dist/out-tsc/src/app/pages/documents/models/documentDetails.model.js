"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var index_model_1 = require("./index.model");
var DocumentDetails = (function (_super) {
    __extends(DocumentDetails, _super);
    function DocumentDetails(documentId, caseId, documentCategoryId, fileTypeId, documentPath, documentContent, createdByUserId, author, caseNumber, documentCategoryName, fileIconPath) {
        var _this = _super.call(this, documentId, caseId, documentCategoryId, fileTypeId, documentPath, documentContent, createdByUserId) || this;
        _this.author = author;
        _this.caseNumber = caseNumber;
        _this.documentCategoryName = documentCategoryName;
        _this.fileIconPath = fileIconPath;
        return _this;
    }
    return DocumentDetails;
}(index_model_1.Document));
exports.DocumentDetails = DocumentDetails;
//# sourceMappingURL=documentDetails.model.js.map