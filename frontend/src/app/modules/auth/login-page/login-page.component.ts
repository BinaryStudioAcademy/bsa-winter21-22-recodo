import { Component, Input, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { UserLoginDto } from 'src/app/models/auth/user-login-dto';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent{

  @Input() userLoginDto:UserLoginDto = {} as UserLoginDto;
  public loginForm: FormGroup;

  public hidePass = true;
  public hideConfirmPass = true;

  constructor(
    private formBuilder: FormBuilder,
    private loginService:LoginService) {
    this.loginForm = this.formBuilder.group({
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
    }); 
  }

  public signIn(_user:UserLoginDto){
    this.loginService.login(_user);
  }

}
