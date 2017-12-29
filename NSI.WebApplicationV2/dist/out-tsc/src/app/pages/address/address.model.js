"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Address = (function () {
    function Address() {
    }
    Address.prototype.Address = function (address_1, address_2, address_id, address_type_id, city, created_by_user_id, date_created, date_modified, is_deleted, zip_code) {
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
    };
    return Address;
}());
exports.Address = Address;
//# sourceMappingURL=address.model.js.map