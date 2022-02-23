import { Component, Input, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { UserLoginDto } from 'src/app/models/auth/user-login-dto';
import { LoginService } from 'src/app/services/login.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserDto } from 'src/app/models/user/user-dto';
import { ExternalAuthService } from 'src/app/services/external-auth.service';
import { SnackBarService } from 'src/app/services/snack-bar.service';

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
    private loginService : LoginService,
    private externalAuthService: ExternalAuthService,
    private snackbarService: SnackBarService
  ) { }

  ngOnInit() {
    this.validateForm();
    this.route.queryParams.subscribe(params => {
      this.redirectUrl = params['redirect_url'];
    });
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
          if (this.redirectUrl)
          {
            this.router.navigate(['/']).then( () => {
            window.location.href= `${this.redirectUrl}?access_token=${localStorage.getItem('accessToken')}`
            });
          }
          else {
            this.router.navigate(['/personal']);
          }
        }
      },
      (error) => {
        switch (error.status) {
          case 401:
            this.snackbarService.openSnackBar('Incorrect password');
            break;
          case 404:
            this.snackbarService.openSnackBar('No user was found with this email');
          }
      });
  }

  public googleLogin = () => {
    this.externalAuthService.signInWithGoogle();
  };
}
