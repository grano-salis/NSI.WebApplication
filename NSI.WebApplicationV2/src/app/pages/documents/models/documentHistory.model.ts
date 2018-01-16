export class DocumentHistory {
    documentPath: string;
    fileTypeIcon: string;
    documentTitle: string;
    caseNumber: string;
    documentCategoryName: string;
    documentDescription: string;
    author: string;
    modifiedAt: Date;
    
    constructor(documentPath: string, fileTypeIcon: string, documentTitle: string, caseNumber: string, documentCategoryName: string,
        documentDescription: string, author: string, modifiedAt: Date) {
        
        this.documentPath = documentPath;
        this.fileTypeIcon = fileTypeIcon;
        this.documentTitle = documentTitle;
        this.caseNumber = caseNumber;
        this.documentCategoryName = documentCategoryName;
        this.documentDescription = documentDescription;
        this.author = author;
        this.modifiedAt = modifiedAt;
    }
}