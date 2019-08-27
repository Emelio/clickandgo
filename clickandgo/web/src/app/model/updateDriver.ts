import { Address } from './Address';


export class UpdateDriver {

    firstName: string;

    middleName: string;

    lastName: string;

    primaryId: string;

    dateIssued: Date;

    dateExpired: Date;

    gender: string;

    class: string;

    trn: string;

    collectorate: string;

    address: Address = new Address();

    dob: Date;

    licenseToDrive: string;

    pPV: string;

    policeRecordNumber: string;

    timePoliceRecordIssue: string;

    informationAccurate: string;

    terms: string;

    vehicleID: string;

    driverID: string;

    deserialize(input: any): this {
        Object.assign(this, input);
        
        this.address = new Address().deserialize(input.address);

        return this;
    } 

}
