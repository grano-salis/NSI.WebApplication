export class DocumentCategory {
    documentCategoryId: number;
    categoryTitle: string;

    constructor(documentCategoryId: number, categoryTitle: string, customerId: number) {

        this.documentCategoryId = documentCategoryId;
        this.categoryTitle = categoryTitle;
    }
}