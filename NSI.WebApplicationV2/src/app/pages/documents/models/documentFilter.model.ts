export class DocumentFilter {
    type: string;
    component: any;
    value: string;

    constructor(type: string, component: any, value: string) {
        this.type = type;
        this.component = component;
        this.value = value;
    }
}