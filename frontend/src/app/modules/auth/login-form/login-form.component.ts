import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SocialAuthService, SocialUser } from 'angularx-social-login';
import { passwordMatchValidator } from 'src/app/core/validators/customValidators';
import { GoogleLoginProvider } from 'angularx-social-login';
import { ExternalAuthService } from 'src/app/services/external-auth.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
})
export class LoginFormComponent {
  public registerForm: FormGroup;

  public hidePass = true;
  public hideConfirmPass = true;

  constructor(
    private formBuilder: FormBuilder,
    private externalAuthService: ExternalAuthService
  ) {
    this.registerForm = this.formBuilder.group(
      {
        email: [
          ,
          {
            validators: [Validators.required, Validators.email],
            updateOn: 'change',
          },
        ],
        password: [
          ,
          {
            validators: [
              Validators.required,
              Validators.minLength(8),
              Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$'),
            ],
            updateOn: 'change',
          },
        ],
      },
      {
        validator: passwordMatchValidator,
      }
    );
  }

  public googleLogin = () => {
    this.externalAuthService.signInWithGoogle();
  };
}
