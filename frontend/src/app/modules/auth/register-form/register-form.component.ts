import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { matchValidator } from '../validators/customValidator';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.scss']
})
export class RegisterFormComponent implements OnInit {

  public registerForm: FormGroup;

  public hidePass: Boolean = true;
  public hideConfirmPass: Boolean = true;

  constructor(
    private formBuilder: FormBuilder
  ) {
    this.registerForm = this.formBuilder.group({
      fullName: [, {
        validators: [Validators.required,
        Validators.pattern("^[a-zA-Z'][a-zA-Z-' ]+[a-zA-Z']?$")
        ], updateOn: "change"
      }],
      email: [, { validators: [Validators.required, Validators.email], updateOn: "change", }],
      password: [, {
        validators: [Validators.required,
        Validators.minLength(8),
        Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$'),
        ], updateOn: "change"
      }],
      confirmPassword: [, { validators: [Validators.required, matchValidator('password')], updateOn: "change" }],
    });
  }

  ngOnInit(): void {

  }


}
