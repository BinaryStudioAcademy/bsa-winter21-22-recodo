import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { passwordMatchValidator } from 'src/app/core/validators/customValidators';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.scss']
})
export class RegisterFormComponent {

  public registerForm: FormGroup;

  public hidePass = true;
  public hideConfirmPass = true;

  constructor(private formBuilder: FormBuilder) {
    this.registerForm = this.formBuilder.group({
      fullName: [, {
        validators: [
          Validators.required,
          Validators.pattern("^[a-zA-Z'][a-zA-Z-' ]+[a-zA-Z']?$")
        ],
        updateOn: "change"
      }],
      email: [, {
        validators: [
          Validators.required,
          Validators.email
        ],
        updateOn: "change",
      }],
      password: [, {
        validators: [
          Validators.required,
          Validators.minLength(8),
          Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$'),
        ],
        updateOn: "change"
      }],
      confirmPassword: [, {
        validators: [
          Validators.required
        ],
        updateOn: "change"
      }],
    }, {
      validator: passwordMatchValidator
    });
  }

}
