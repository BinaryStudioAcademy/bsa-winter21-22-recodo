import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthUserDto } from '../models/auth/auth-user-dto';
import { UserDto } from '../models/user/user-dto';
import { UserUpdateDto } from '../models/user/user-update-dto';
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
    return this.addWithUrl<{}, {}>(`Resetpassword/${email}`, {
      email,
    });
  }

  public resetPasswordFinish(email: string, password: string) {
    return this.addWithUrl<{}, AuthUserDto>(
      `ResetPasswordFinish/${email}/${password}`,
      {
        email,
        passwordNew: password,
      }
    );
  }

  public deleteUser(subUrl: string) {
    return this.addWithUrl<{}, {}>(subUrl, {});
  }

  public updateInfo(subUrl: string, data: FormData) {
    return this.addWithUrl<FormData, {}>(subUrl, data);
  }

  public updatePassword(subUrl: string, data: UserUpdateDto) {
    return this.addWithUrl<UserUpdateDto, {}>(subUrl, data);
  }
}
