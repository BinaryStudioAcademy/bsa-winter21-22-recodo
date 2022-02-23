import { Component, Input, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { UserLoginDto } from 'src/app/models/auth/user-login-dto';
import { LoginService } from 'src/app/services/login.service';
import { Router } from '@angular/router';
import { UserDto } from 'src/app/models/user/user-dto';
import { ExternalAuthService } from 'src/app/services/external-auth.service';

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

  constructor(
    private router : Router,
    private formBuilder : FormBuilder,
    private loginService : LoginService,
    private externalAuthService: ExternalAuthService
  ) { }

  ngOnInit() {
    this.validateForm();
  }

  private validateForm() {
    this.loginForm = this.formBuilder.group({
      email: [, [
          Validators.required,
          Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')
        ]
      ],
      password: [, [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(20),
          Validators.pattern(/^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9*.!@#$%^&`(){}[\]:;<>,‘.?/~_+=|-]+)$/)
        ],
      ],
    });
  }

  public signIn(_user: UserLoginDto) {
    this.loginService.login(_user)
      .subscribe((response) => {
        this.currentUser = response;
        if(this.loginService.areTokensExist()) {
          this.router.navigate(['/personal']);
        }
      },
      (error) => {
        if(error.status === 404) {
          alert('No user was found with this email');
        }
        else if(error.status === 401) {
          alert('Incorrect password');
        }
        else {
          alert('Error');
        }
      });
  }

  public googleLogin = () => {
    this.externalAuthService.signInWithGoogle();
  };
}
