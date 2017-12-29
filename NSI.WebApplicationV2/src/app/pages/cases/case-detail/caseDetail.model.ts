export class CaseDetail {
    
        caseId: number;
        caseNumber: string;
        courtNumber:string;
        value : number;
        judge :string;
        court:string;
        counterParty:string;
        caseCategory : number;
        dateCreated: Date;
        note:any;
        
    
        constructor(caseId:number,caseNumber:string,courtNumber:string,value : number,judge :string,court:string,
            counterParty:string,caseCategory : number,dateCreated:Date,note:any){
            
        }
    
    }
    