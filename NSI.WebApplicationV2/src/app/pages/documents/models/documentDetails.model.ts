import { Document } from "./index.model";

export class DocumentDetails extends Document {
    author: string;
    caseNumber: number;
    documentCategoryName: string;
    fileIconPath: string;
    createdAt: string;
    modifiedAt: string;
    
    constructor(documentId: number, title: string, description: string, caseId: number, documentCategoryId: number, fileTypeId: number, 
        documentPath: string, documentContent: string, createdByUserId: number, author: string, 
        caseNumber: number, documentCategoryName: string, fileIconPath: string, createdAt: string, modifiedAt: string) {

        super(documentId, title, description, caseId, documentCategoryId, fileTypeId, documentPath, documentContent, createdByUserId);
        this.author = author;
        this.caseNumber = caseNumber;
        this.documentCategoryName = documentCategoryName;
        this.fileIconPath = fileIconPath;
        this.createdAt = createdAt;
        this.modifiedAt = modifiedAt;
    }
}