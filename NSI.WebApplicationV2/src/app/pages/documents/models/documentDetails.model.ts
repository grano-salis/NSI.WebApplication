import { Document } from "./document.model";

export class DocumentDetails extends Document {
    fileTypeId: number;        
    author: string;
    caseNumber: number;
    documentCategoryName: string;
    fileIconPath: string;
    createdAt: string;
    modifiedAt: string;
    
    constructor(documentId: number, title: string, description: string, caseId: number, categoryId: number, documentContent: string,
        createdByUserId: number, fileTypeId: number, documentPath: string, author: string, caseNumber: number, 
        documentCategoryName: string, fileIconPath: string, createdAt: string, modifiedAt: string) {

        super(documentId, title, description, caseId, categoryId, documentContent, createdByUserId, documentPath);
        this.fileTypeId = fileTypeId;        
        this.documentPath = documentPath;
        this.author = author;
        this.caseNumber = caseNumber;
        this.documentCategoryName = documentCategoryName;
        this.fileIconPath = fileIconPath;
        this.createdAt = createdAt;
        this.modifiedAt = modifiedAt;
    }
}