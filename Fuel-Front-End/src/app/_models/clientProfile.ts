export interface ClientProfile {
    id: number;
    fullname: string;
    photoURL: string;
    address1: string;
    address2?: string;
    city: string;
    state: string;
    zipcode: string;
}
