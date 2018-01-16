export class DocumentFilter {
    type: string;
    component: any;
    field: string;
    value: any;

    constructor(type: string, component: any, field: string, value: any) {
        this.type = type;
        this.component = component;
        this.field = field;
        this.value = value;
    }
}