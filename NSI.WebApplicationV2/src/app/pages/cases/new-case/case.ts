import * as moment from "moment";
import _date = moment.unitOfTime._date;

export class Case {

  caseNumber: string;
  courtNumber: string;
  value: number;
  judge: string;
  court: string;
  counterParty: string;
  note: string;
  dateCreated: any;
  dateModified: null;
  caseCategory: number;
  customerId: number;
  clientId: number;
  createdByUserId: number;

  
  constructor() {
  }
}
