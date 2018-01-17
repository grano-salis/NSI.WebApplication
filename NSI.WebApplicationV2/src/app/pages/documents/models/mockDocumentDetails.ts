import { DocumentDetails } from './index.model';

export var MDD: DocumentDetails[] = [
    {
        documentId: 1, 
        documentTitle: "NSI dokumentacija.docx",
        documentDescription: "Description1 ...",
        caseId: 1,
        categoryId: 1,
        fileTypeId: 1,
        documentPath: "C:",
        documentContent: "...",
        createdByUserId: 1,
        author: "Amir",
        caseNumber: 12345678,
        documentCategoryName: "Invoice",
        fileIconPath: "fa-file-word-o",
        createdAt: new Date().toLocaleString(),
        modifiedAt: new Date().toLocaleString()
    },
    {
        documentId: 2, 
        documentTitle: "NSI erd.jpg",
        documentDescription: "Description2 ...",
        caseId: 1,
        categoryId: 1,
        fileTypeId: 1,
        documentPath: "C:",
        documentContent: "",
        createdByUserId: 1,
        author: "Amir",
        caseNumber: 12345678,
        documentCategoryName: "Evidence",
        fileIconPath: "fa-file-image-o",
        createdAt: new Date().toLocaleString(),
        modifiedAt: new Date().toLocaleString()
    },
    {
        documentId: 3, 
        documentTitle: "NSI biljeske.txt",
        documentDescription: "Description3 ...",
        caseId: 1,
        categoryId: 1,
        fileTypeId: 1,
        documentPath: "C:",
        documentContent: "",
        createdByUserId: 1,
        author: "Amir",
        caseNumber: 12345678,
        documentCategoryName: "Evidence",
        fileIconPath: "fa-file-text",
        createdAt: new Date().toLocaleString(),
        modifiedAt: new Date().toLocaleString()
    }
];