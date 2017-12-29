"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AddressType = (function () {
    function AddressType() {
    }
    AddressType.prototype.AddressType = function (address_type_id, address_type_name, created_date, modified_date, is_deleted, customerId) {
        this.addressTypeId = address_type_id;
        this.addressTypeName = address_type_name;
        this.createdDate = created_date;
        this.modifiedDate = modified_date;
        this.isDeleted = is_deleted;
        this.customerId = customerId;
    };
    return AddressType;
}());
exports.AddressType = AddressType;
//# sourceMappingURL=addressType.model.js.map