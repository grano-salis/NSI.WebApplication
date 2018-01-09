export class DocumentHistory {
    documentPath: string;
    documentTitle: string;
    caseNumber: string;
    documentCategoryName: string;
    documentDescription: string;
    author: string;
    modifiedAt: Date;
    
    constructor(documentPath: string, documentTitle: string, caseNumber: string, documentCategoryName: string,
        documentDescription: string, author: string, modifiedAt: Date) {
        
        this.documentPath = documentPath;
        this.documentTitle = documentTitle;
        this.caseNumber = caseNumber;
        this.documentCategoryName = documentCategoryName;
        this.documentDescription = documentDescription;
        this.author = author;
        this.modifiedAt = modifiedAt;
    }
}