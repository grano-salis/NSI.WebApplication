export class Customer {

	customerId?: number;
	customerName: string;
	logoLink?: string;
	dateCreated?: Date;
	pricingPackageId: number;
	addressId: number;

	constructor(customerName: string,  addressId: number, pricingPackageId: number, dateCreated?: Date, customerId?: number) {
	}

}
