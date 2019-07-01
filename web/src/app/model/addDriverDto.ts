import { Address } from './Address';

export class AddDriverDto {

    FirstName: string;

    MiddleName: string;

    LastName: string;

    PrimaryId: string;

    DateIssued: Date;

    DateExpired: Date;

    Gender: string;

    Class: string;

    Trn: string;

    Collectorate: string;

    Address: Address;

    DOB: Date;

    LicenseToDrive: string;

    PPV: string;

    PoliceRecordNumber: string;

    TimePoliceRecordIssue: Date;

    InformationAccurate: string

    Terms: string;

    deserialize(): this {
        //Object.assign(this, input);
        
        this.Address = new Address().deserialize(this.Address);

        return this;
    } 
}
