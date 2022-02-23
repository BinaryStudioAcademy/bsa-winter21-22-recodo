import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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
export class RegisterFormComponent {
  public registerForm: FormGroup = {} as FormGroup;

  public hidePass = true;
  public hideConfirmPass = true;
  public currentUser: UserDto = {} as UserDto;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private registrationService: RegistrationService,
    private externalAuthService: ExternalAuthService
  ) {
    this.validateForm();
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
          Validators.pattern(/^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9*.!@#$%^&`(){}[\]:;<>,‘.?/~_+=|-]+)$/),
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
      if (this.registrationService.areTokensExist()) {
        this.router.navigate(['/login']);
      }
    },(error) => {
      if(error.status === 401) {
        alert('This email already exists');
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
