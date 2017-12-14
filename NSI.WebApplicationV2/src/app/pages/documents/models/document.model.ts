export class Document {
    documentId: number;
    documentTitle: string;
    documentDescription: string;
    caseId: number;
    documentCategoryId: number;
    fileTypeId: number;     
    documentPath: string;
    documentContent: string;
    createdByUserId: number;

    constructor(documentId: number, title: string, description: string, caseId: number, documentCategoryId: number, fileTypeId: number,
        documentPath: string, documentContent: string, createdByUserId: number) {

        this.documentId = documentId;
        this.documentTitle = title;
        this.documentDescription = description;
        this.caseId = caseId;
        this.documentCategoryId = documentCategoryId;
        this.fileTypeId = fileTypeId;        
        this.documentPath = documentPath;
        this.documentContent = documentContent;
        this.createdByUserId = createdByUserId;
    } 
}