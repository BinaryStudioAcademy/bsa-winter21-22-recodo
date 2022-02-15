import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { AuthUserDto } from '../models/auth/auth-user-dto';
import { UserLoginDto } from '../models/auth/user-login-dto';
import { TokenDto } from '../models/token/token-dto';
import { UserDto } from '../models/user/user-dto';
import { ResourceService } from './resource.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService extends ResourceService<UserLoginDto> {

  private user:UserDto | undefined= {} as UserDto;

  constructor(override httpClient: HttpClient) {
    super(httpClient);
  }

  getResourceUrl(): string {
    return '/Login';
  }

  public login(user : UserLoginDto) {
    return this._handleAuthResponse(this.add<UserLoginDto,AuthUserDto>(user));
  }

  public areTokensExist() {
    return localStorage.getItem('accessToken') && localStorage.getItem('refreshToken');
  }

  private _handleAuthResponse(observable: Observable<HttpResponse<AuthUserDto>>) {
    return observable.pipe(
        map((resp) => {
            this._setTokens(resp.body?.token);
            this.user = resp.body?.user;
            return resp.body?.user;
        })
    );
  }

  private _setTokens(tokens: TokenDto | undefined) {
    if (tokens && tokens.accessToken && tokens.refreshToken) {
        localStorage.setItem('accessToken', JSON.stringify(tokens.accessToken));
        localStorage.setItem('refreshToken', JSON.stringify(tokens.refreshToken));
    }
  } 
}
