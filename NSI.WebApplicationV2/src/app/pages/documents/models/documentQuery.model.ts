export class DocumentQuery {
    search: string;
    pageNumber: number;
    resultsPerPage: number;
    filterBy: string;

    constructor(search: string, pageNumber: number, resultsPerPage: number, filterBy: string) {

        this.search = search;
        this.pageNumber = pageNumber;
        this.resultsPerPage = resultsPerPage;
        this.filterBy = filterBy;
        
    }
}