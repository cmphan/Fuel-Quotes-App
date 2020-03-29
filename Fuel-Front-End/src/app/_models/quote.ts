import { NumberValueAccessor } from '@angular/forms';

export interface Quote {
    id: number;
    gallonsRequested: number;
    deliveryAddress: string;
    deliveryDate: Date;
    suggestedPrice: number;
    amountDue: number;
}
