import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { UserDto } from '../models/user/user-dto';
import { ResourceService } from './resource.service';

@Injectable({
  providedIn: 'root',
})
export class UserService extends ResourceService<UserDto> {
  private subUrl: string = '';

  constructor(override httpClient: HttpClient, private router: Router) {
    super(httpClient);
  }

  setSubUrl(url: string) {
    this.subUrl = url;
  }

  getResourceUrl(): string {
    if (this.subUrl) {
      return '/User' + this.subUrl;
    }
    return '/User';
  }

  public resetPassword(email: string) {
    return this.addWithUrl<{ email: string }, {}>('Resetpassword/', {
      email,
    });
  }

  public resetPasswordDone(email: string, password: string) {
    return this.addWithUrl<{ email: string; passwordNew: string }, {}>(
      'ResetPasswordDone/',
      {
        email,
        passwordNew: password,
      }
    );
  }
}
