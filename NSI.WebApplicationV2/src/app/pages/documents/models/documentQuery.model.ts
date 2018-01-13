export class DocumentQuery {
    pageNumber: number;
    resultsPerPage: number;

    searchByTitle: string;
    searchByCaseId: number;
    searchByCategoryId: number;
    searchByDescription: string;

    createdDateFrom: any;
    createDateTo: any;
    modifiedDateFrom: any;
    modifiedDateTo: any;

    constructor(pageNumber: number, resultsPerPage: number) {

        this.pageNumber = pageNumber;
        this.resultsPerPage = resultsPerPage;

        this.searchByTitle = "";
        this.searchByCaseId = 0;
        this.searchByCategoryId = 0;
        this.searchByDescription = "";

        this.createdDateFrom = null;
        this.createDateTo = null;
        this.modifiedDateFrom = null;
        this.modifiedDateTo = null;        
    }
}