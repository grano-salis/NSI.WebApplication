import { Address } from "../../address/address.model";
import { PricingPackage } from "./pricing-package";

export class Customer {

	customerId: number;
	customerName: string;
	logoLink: string;
	dateCreated: Date;
	pricingPackage: PricingPackage;
	address: Address;

	constructor(customerId: number, customerName: string, dateCreated: Date, pricingPackage: any, address: any) {
	}

}
