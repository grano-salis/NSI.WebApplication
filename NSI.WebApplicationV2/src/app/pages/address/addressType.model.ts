export class AddressType {

  addressTypeId: number;
  addressTypeName: string;
  createdDate: Date;
  modifiedDate: Date;
  isDeleted: boolean;
  customerId: number;

  private AddressType(address_type_id: number, address_type_name: string, created_date: Date, modified_date: Date, is_deleted: boolean, customerId: number) {
    this.addressTypeId = address_type_id;
    this.addressTypeName = address_type_name;
    this.createdDate = created_date;
    this.modifiedDate = modified_date;
    this.isDeleted = is_deleted;
    this.customerId = customerId;



  }
}
