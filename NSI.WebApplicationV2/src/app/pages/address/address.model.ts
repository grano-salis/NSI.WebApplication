
export class Address {


  address1: string;
  address2: string;
  addressId: number;
  addressTypeId: number;
  city: string;
  createdByUserId: number;
  dateCreated: string;
  dateModified: string;
  isDeleted: boolean;
  zipCode: number;

  public Address(address_1: string, address_2: string, address_id: number, address_type_id: number, city: string, created_by_user_id: number, date_created: string, date_modified: string, is_deleted: boolean, zip_code: number) {
    this.addressId = address_id;
    this.address1 = address_1;
    this.address2 = address_2;
    this.addressTypeId = address_type_id;
    this.city = city;
    this.createdByUserId = created_by_user_id;
    this.dateCreated = date_created;
    this.dateModified = date_modified;
    this.isDeleted = is_deleted;
    this.zipCode = zip_code;
  }
}
