import { ClientProfile } from './clientProfile';

export interface User {
    userId: number;
    username: string;
    dateCreated: Date;
    lastActive: Date;
    photoURL?: string;
    clientProfile?: ClientProfile;
}
