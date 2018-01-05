export class DocumentQuery {
    pageNumber: number;
    resultsPerPage: number;

    searchByTitle: string;
    searchByCaseId: number;
    searchByCategoryId: number;
    searchByDescription: string;

    createdDateFrom: Date;
    createDateTo: Date;
    ModifiedDateFrom: Date;
    ModifiedDateTo: Date;

    constructor(pageNumber: number, resultsPerPage: number) {

        this.pageNumber = pageNumber;
        this.resultsPerPage = resultsPerPage;

        this.searchByTitle = "";
        this.searchByCaseId = 0;
        this.searchByCategoryId = 0;
        this.searchByDescription = "";
        
    }
}