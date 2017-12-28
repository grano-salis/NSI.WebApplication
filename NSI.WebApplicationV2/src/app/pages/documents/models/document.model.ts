export class Document {
    documentId: number;
    documentTitle: string;
    documentDescription: string;
    caseId: number;
    categoryId: number;
    documentContent: string;
    createdByUserId: number;
    documentPath: string; 

    constructor(documentId: number, title: string, description: string, caseId: number, categoryId: number, 
        documentContent: string, createdByUserId: number, documentPath: string) {

        this.documentId = documentId;
        this.documentTitle = title;
        this.documentDescription = description;
        this.caseId = caseId;
        this.categoryId = categoryId;
        this.documentContent = documentContent;
        this.createdByUserId = createdByUserId;
        this.documentPath = documentPath;
    } 
}