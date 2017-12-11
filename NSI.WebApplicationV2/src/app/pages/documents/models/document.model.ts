export class Document {
    documentId: number;
    caseId: number;
    documentCategoryId: number;
    fileTypeId: number;     
    documentPath: string;
    documentContent: string;
    createdByUserId: number;

    constructor(documentId: number, caseId: number, documentCategoryId: number, fileTypeId: number,
        documentPath: string, documentContent: string, createdByUserId: number) {

        this.documentId = documentId;
        this.caseId = caseId;
        this.documentCategoryId = documentCategoryId;
        this.fileTypeId = fileTypeId;        
        this.documentPath = documentPath;
        this.documentContent = documentContent;
        this.createdByUserId = createdByUserId;
    } 
}