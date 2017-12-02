
export class Address {
  address_1: string;
  address_2: string;
  addres_id: number;
  address_type_id: number;
  city: string;
  created_by_user_id: number;
  date_created: string;
  date_modfied: string;
  is_deleted: boolean;
  zip_code: number;
  public Address(address_1: string, address_2: string, address_id: number,
                 address_type_id: number, city: string, created_by_user_id: number,
                 date_created: string, date_modified: string, is_deleted: boolean, zip_code: number) {
    this.addres_id = address_id;
    this.address_1 = address_1;
    this.address_2 = address_2;
    this.address_type_id = this.address_type_id;
    this.city = city;
    this.created_by_user_id = this.created_by_user_id;
    this.date_created = this.date_created;
    this.date_modfied = this.date_modfied;
    this.is_deleted = this.is_deleted;
    this.zip_code = this.zip_code;
  }
}
