export class Address {

    street: string;

    district: string;

    city: string;

    parish: string;

    country: string;

    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}
