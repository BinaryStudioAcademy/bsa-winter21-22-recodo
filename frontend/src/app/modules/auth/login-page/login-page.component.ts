import { Component, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { passwordMatchValidator } from 'src/app/core/validators/customValidators';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent{

  public loginForm: FormGroup;

  public hidePass = true;
  public hideConfirmPass = true;

  constructor(private formBuilder: FormBuilder) {
    this.loginForm = this.formBuilder.group({
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
