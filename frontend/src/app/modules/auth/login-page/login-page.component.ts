import { Component, Input, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { UserLoginDto } from 'src/app/models/auth/user-login-dto';
import { LoginService } from 'src/app/services/login.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserDto } from 'src/app/models/user/user-dto';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {

  @Input() userLoginDto : UserLoginDto = {} as UserLoginDto;
  public loginForm : FormGroup = {} as FormGroup;

  public hidePass = true;
  public hideConfirmPass = true;
  public currentUser:UserDto = {} as UserDto;
  redirectUrl : string | undefined;

  constructor(
    private router : Router,
    private route : ActivatedRoute,
    private formBuilder : FormBuilder,
    private loginService : LoginService
  ) {}

  ngOnInit() {
    this.validateForm();
    this.route.queryParams.subscribe(params => {
      this.redirectUrl = params['redirect_url'];
    });
  }

  private validateForm() {
    this.loginForm = this.formBuilder.group({
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
    });
  }

  public signIn(_user : UserLoginDto) {
    this.loginService.login(_user).subscribe((responce) => {
      this.currentUser = responce;
      if(this.loginService.areTokensExist()) {
        if (this.redirectUrl)
        {
          window.location.href= `${this.redirectUrl}?access_token=${localStorage.getItem('accessToken')}`;
        }
        else {
          this.router.navigate(['/personal']);
        }
      }
    });
  }
}
