export class ValidationService {
  static getValidatorErrorMessage(validatorName: string, validatorValue?: any) {
    let config = {
      'required': 'Required',
      'invalidEmailAddress': 'Invalid email address format. Example: someone@domain.com',
      'lettersOnlyInvalid': 'Should contain letters only.',
      'invalidNumbersOnly' : 'Should contain numbers only'
    };

    return config[validatorName];
  }

  static lettersOnlyValidator(control:any){
    if (control.value.match(/^[a-zA-Z]+$/)){
      return null;
    }
    else {
      return {'lettersOnlyInvalid':true};
    }
  }

  static numbersOnlyValidator (control:any){
    if (control.value.match(/^[0-9]*$/)){
      return null;
    }
    else {
      return {'invalidNumbersOnly':true};
    }
  }

  static emailValidator(control:any) {
    // RFC 2822 compliant regex
    if (control.value.match(/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/)) {
      return null;
    } else {
      return { 'invalidEmailAddress': true };
    }
  }

}
