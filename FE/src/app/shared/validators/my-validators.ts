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
      return isValid ? null : { number: true };
    };
  }

  static passwordMatch(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const password = control.get('password');
      const repassword = control.get('repassword');
      if (password && repassword) {
        const isMatch = password.value === repassword.value;
        console.log('ismatch:', isMatch);
        return isMatch ? null : { passwordsNotMatch: true };
      }
      return null;
    };
  }
}
