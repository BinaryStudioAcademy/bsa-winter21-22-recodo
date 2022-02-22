import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import {
  cannotContainSpace,
  passwordMatchValidator,
  startsOrEndWithSpace} from 'src/app/core/validators/customValidators';
import { UserDto } from 'src/app/models/user/user-dto';
import { UserRegisterDto } from 'src/app/models/user/user-register-dto';
import { ExternalAuthService } from 'src/app/services/external-auth.service';
import { RegistrationService } from 'src/app/services/registration.service';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.scss'],
})
export class RegisterFormComponent implements OnInit {

  public registerForm: FormGroup = {} as FormGroup;

  public hidePass = true;
  public hideConfirmPass = true;
  public currentUser:UserDto = {} as UserDto;
  redirectUrl : string | undefined;

  constructor(
    private router : Router,
    private route : ActivatedRoute,
    private formBuilder: FormBuilder,
    private registrationService: RegistrationService) {
  }

  ngOnInit() {
    this.validateForm();
    this.route.queryParams.subscribe(params => {
      this.redirectUrl = params['redirect_url'];
    });
  }

  private validateForm() {
    this.registerForm = this.formBuilder.group({
      workspaceName: [,
          [Validators.required,
          Validators.pattern('^[а-яА-ЯёЁa-zA-Z\`\'][а-яА-ЯёЁa-zA-Z-\`\' ]+[а-яА-ЯёЁa-zA-Z\`\']?$'),
          Validators.minLength(3),
          Validators.maxLength(30),
          startsOrEndWithSpace
        ],
      ],
      email: [, [
          Validators.required,
          Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
        ]
      ],
      confirmPassword: [, [
          Validators.required,
        ],
      ],
      password: [, [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(20),
          Validators.pattern(
            '^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9*.!@\\#$%^&`\\(\\)\\{\\}\\[\\]\\\\:;<>,‘.?/~_+-=|]+)$'
            ),
          cannotContainSpace
        ],
      ],
    }, {
      validator: passwordMatchValidator
    });
  }

  public registerUser() {
    let userRegisterDto: UserRegisterDto = {
      email: this.registerForm.controls['email'].value,
      workspaceName: this.registerForm.controls['workspaceName'].value,
      password: this.registerForm.controls['password'].value,
    };
    this.registrationService.register(userRegisterDto).subscribe((responce) => {
      this.currentUser = responce;
      if(this.registrationService.areTokensExist()) {
        if (this.redirectUrl)
        {
          this.router.navigate(['/']).then( () => {
            window.location.href= `${this.redirectUrl}?access_token=${localStorage.getItem('accessToken')}`
          });
        }
        else {
          this.router.navigate(['/login']);
        }
      }
    });
  }

  public googleLogin = () => {
    this.externalAuthService.signInWithGoogle();
  };
}
