import { Component, Input, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { UserLoginDto } from 'src/app/models/auth/user-login-dto';
import { LoginService } from 'src/app/services/login.service';
import { Router } from '@angular/router';
import { UserDto } from 'src/app/models/user/user-dto';
import { ExternalAuthService } from 'src/app/services/external-auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-reset-pass',
  templateUrl: './reset-pass-page.component.html',
  styleUrls: ['./reset-pass-page.component.scss'],
})
export class ResetPassPageComponent implements OnInit {
  @Input() userLoginDto: UserLoginDto = {} as UserLoginDto;
  public loginForm: FormGroup = {} as FormGroup;

  public hidePass = true;
  public hideConfirmPass = true;
  public currentUser: UserDto = {} as UserDto;
  public isDone: boolean = false;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private loginService: LoginService,
    private externalAuthService: ExternalAuthService,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.validateForm();
  }

  private validateForm() {
    this.loginForm = this.formBuilder.group({
      email: [
        ,
        [
          Validators.required,
          Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
        ],
      ],
    });
  }

  public resetPassword(_user: UserLoginDto) {
    let email = _user.email;
    this.userService.resetPassword(email).subscribe(() => {
      this.isDone = true;
    });
  }
}
