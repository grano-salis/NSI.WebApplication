export class Hearing {

    hearingDate: string;
    createdByUserId: number;
    caseId: number;
    userHearing: any[];
    note: any[];

    constructor() {
        this.userHearing = [];
        this.note = [];
    }
}