import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { passwordMatchValidator } from 'src/app/core/validators/customValidators';
import {UserRegisterDto} from 'src/app/models/user/user-register-dto';
import { RegistrationService } from 'src/app/services/registration.service';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.scss']
})
export class RegisterFormComponent {

  public registerForm: FormGroup;

  public hidePass = true;
  public hideConfirmPass = true;

  constructor(private formBuilder: FormBuilder,
    private registrationService: RegistrationService) {
    this.registerForm = this.formBuilder.group({
      fullName: [, {
        validators: [
          Validators.required,
          Validators.pattern('^[a-zA-Z\'][a-zA-Z-\' ]+[a-zA-Z\']?$')
        ],
        updateOn: 'change'
      }],
      email: [, {
        validators: [
          Validators.required,
          Validators.email
        ],
        updateOn: 'change',
      }],
      password: [, {
        validators: [
          Validators.required,
          Validators.minLength(8),
          Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$'),
        ],
        updateOn: 'change'
      }],
      confirmPassword: [, {
        validators: [
          Validators.required
        ],
        updateOn: 'change'
      }],
    }, {
      validator: passwordMatchValidator
    });
  }
  registerUser() {
    let userRegisterDto : UserRegisterDto = {
      email: this.registerForm.controls['email'].value,
      userName: this.registerForm.controls['fullName'].value,
      password: this.registerForm.controls['password'].value
    }
    this.registrationService.register(userRegisterDto)
  }
}
