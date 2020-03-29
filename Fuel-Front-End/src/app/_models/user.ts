import { ClientProfile } from './clientProfile';
import { Quote } from './quote';

export interface User {
    userId: number;
    username: string;
    dateCreated: Date;
    lastActive: Date;
    photoURL?: string;
    clientProfile?: ClientProfile;
    quote?: Quote[];
}
