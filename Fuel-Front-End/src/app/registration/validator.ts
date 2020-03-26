import {AbstractControl, FormGroup} from '@angular/forms';
export function passValidator(control: AbstractControl){
    if (control && (control.value !=null || control.value != undefined)) {
        const confpassValue = control.value;
        const passControl = control.root.get('password');
        if (passControl) {
            const passValue = passControl.value;
            if (passValue !== confpassValue) {
                return {
                    isError: true }; }
        }
    }
    return null;
}