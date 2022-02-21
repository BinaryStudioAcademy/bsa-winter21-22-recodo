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

  public resetPassword(subUrl: string, user: { id: string }) {
    return this.addWithUrl<{ id: string }, {}>(subUrl, user);
  }

  public deleteUser(subUrl: string, user: { id: string }) {
    return this.addWithUrl<{ id: string }, {}>(subUrl, user);
  }

  public updateInfo(subUrl: string, data: FormData) {
    return this.addWithUrl<FormData, {}>(subUrl, data);
  }
}
