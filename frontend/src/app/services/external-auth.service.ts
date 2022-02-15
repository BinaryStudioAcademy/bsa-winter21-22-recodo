import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GoogleLoginProvider } from 'angularx-social-login';
import { SocialAuthService, SocialUser } from 'angularx-social-login';
import { environment } from '../../environments/environment';
import { IAuthUserDTO } from '../core/dtos/auth-user.dto';
@Injectable({
  providedIn: 'root',
})
export class ExternalAuthService {
  private readonly APIUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private googleAuthService: SocialAuthService
  ) {}

  public signInWithGoogle = () => {
    this.googleAuthService.signIn(GoogleLoginProvider.PROVIDER_ID).then(
      (res) => {
        const user: SocialUser = { ...res };
        this.validateExternalAuth(user);
      },
      (error) => {
        console.log(error);
      }
    );
  };

  public signOutExternal = () => {
    this.googleAuthService.signOut();
  };

  private validateExternalAuth(user: SocialUser) {
    let auth = { provider: user.provider, idToken: user.idToken };
    this.http.post<IAuthUserDTO>(`${this.APIUrl}/GoogleLogin`, auth).subscribe({
      next: (data: IAuthUserDTO) => {
        console.log(data);
        alert(data.token);
      },
      error: (e) => {
        this.signOutExternal();
        console.log(e);
      },
    });
  }
}
