import { Document } from "./index.model";

export class DocumentDetails extends Document {
    author: string;
    caseNumber: number;
    documentCategoryName: string;
    fileIconPath: string;

    constructor(documentId: number, caseId: number, documentCategoryId: number, fileTypeId: number, 
        documentPath: string, documentContent: string, createdByUserId: number, author: string, 
        caseNumber: number, documentCategoryName: string, fileIconPath: string) {

        super(documentId, caseId, documentCategoryId, fileTypeId, documentPath, documentContent, createdByUserId);
        this.author = author;
        this.caseNumber = caseNumber;
        this.documentCategoryName = documentCategoryName;
        this.fileIconPath = fileIconPath;
    }
}