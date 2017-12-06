export class AddressType {

  addressTypeId: number;
  addressTypeName: string;
  createdDate: Date;
  modifiedDate: Date;

  private AddressType(address_type_id: number, address_type_name: string, created_date: Date, modified_date: Date) {
    this.addressTypeId = address_type_id;
    this.addressTypeName = address_type_name;
    this.createdDate = created_date;
    this.modifiedDate = modified_date;
  }
}
