import {Address} from "../../address/address.model";
export class Contact {

  taskId: number;
  firsttName: string;
  lastName: string;
  phone: string;
  mobile: string;
  email: string;
  address: Address;
  addressId: number;
  createdDate: Date;
  modifiedDate: Date;
  isDeleted: boolean;
  createdByUserId: number;
  emails: any[];
  phones: any[];
}
