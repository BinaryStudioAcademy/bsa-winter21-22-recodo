import { AbstractControl, ValidationErrors } from '@angular/forms';

export function passwordMatchValidator(control: AbstractControl) {
    const password: string = control.get('password')?.value; // get password from our password form control
    const confirmPassword: string = control.get('confirmPassword')?.value; // get confirmPassword from our form control
    // compare is the password math
    if (password !== confirmPassword) {
        // if they don't match, set an error in our confirmPassword form control
        control.get('confirmPassword')?.setErrors({ NoPassswordMatch: true });
    }
}

export function cannotContainSpace(control: AbstractControl) : ValidationErrors | null {
    if((control.value as string)?.indexOf(' ') >= 0) {
        return {cannotContainSpace: true}
    }

    return null;
}

export function startsOrEndWithSpace(control: AbstractControl) : ValidationErrors | null {
    if(
        (control.value as string)?.startsWith(' ') ||
        (control.value as string)?.endsWith(' ')) {
        return {startsOrEndWithSpace: true}
    }

    return null;
}
