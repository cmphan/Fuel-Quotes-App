import { ClientProfile } from './clientProfile';
import { Quote } from '@angular/compiler';

export interface User {
    userId: number;
    username: string;
    dateCreated: Date;
    lastActive: Date;
    photoURL?: string;
    clientProfile?: ClientProfile;
    Quote?: Quote[];
}
