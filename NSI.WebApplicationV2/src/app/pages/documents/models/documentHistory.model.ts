export class DocumentHistory {
    documentHistoryId: number;        
    modifiedByUserId: number;
    modifiedAt: string;
    documentId: number;
    modifiedByUser: string;
    
    constructor(documentHistoryId: number, modifiedByUserId: number, modifiedAt: string, 
        documentId: number, modifiedByUser: string) {

        this.documentHistoryId = documentHistoryId;
        this.modifiedByUserId = modifiedByUserId;
        this.modifiedAt = modifiedAt;
        this.documentId = documentId;
        this.modifiedByUser = modifiedByUser;
    }
}