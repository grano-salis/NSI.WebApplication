export class DocumentHistory {
    documentPath: string;
    iconPath: string;
    documentTitle: string;
    caseNumber: string;
    documentCategoryName: string;
    documentDescription: string;
    author: string;
    modifiedAt: Date;
    
    constructor(documentPath: string, iconPath: string, documentTitle: string, caseNumber: string, documentCategoryName: string,
        documentDescription: string, author: string, modifiedAt: Date) {
        
        this.documentPath = documentPath;
        this.iconPath = iconPath;
        this.documentTitle = documentTitle;
        this.caseNumber = caseNumber;
        this.documentCategoryName = documentCategoryName;
        this.documentDescription = documentDescription;
        this.author = author;
        this.modifiedAt = modifiedAt;
    }
}