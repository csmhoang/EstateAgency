import {
  ValidatorFn,
  AbstractControl,
  ValidationErrors,
  FormGroup,
} from '@angular/forms';

export class MyValidators {
  static letter(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const isValid = /^[^\d\n`~!@#$%^&*()\-_=+\[{}\]\\|;:'",<\.>/?]*$/.test(
        control.value
      );
      return isValid ? null : { letter: true };
    };
  }

  static phone(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const isValid = /^0[3-9]\d{8}$/.test(control.value);
      return isValid ? null : { phone: true };
    };
  }

  static passwordMatch(passwordNeedMatch: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const password = control.parent?.get(passwordNeedMatch);
      if (password && control && password.value !== control.value) {
        return { passwordMatch: true };
      }
      return null;
    };
  }
}
