import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GoogleLoginProvider } from 'angularx-social-login';
import { SocialAuthService, SocialUser } from 'angularx-social-login';
@Injectable({
  providedIn: 'root',
})
export class ExternalAuthService {
  constructor(
    private http: HttpClient,
    private externalAuthService: SocialAuthService
  ) {}

  public signInWithGoogle = () => {
    this.externalAuthService
      .signIn(GoogleLoginProvider.PROVIDER_ID)
      .then((res) => {
        const user: SocialUser = { ...res };
        console.log(user);
      });
  };

  public signOutExternal = () => {
    this.externalAuthService.signOut();
  };

  private validateExternalAuth(provider: string, idToken: string) {}
}
