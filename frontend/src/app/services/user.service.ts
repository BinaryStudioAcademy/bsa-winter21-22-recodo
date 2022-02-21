import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { UserDto } from '../models/user/user-dto';
import { ResourceService } from './resource.service';

@Injectable({
  providedIn: 'root',
})
export class UserService extends ResourceService<UserDto> {
  constructor(override httpClient: HttpClient, private router: Router) {
    super(httpClient);
  }

  getResourceUrl(): string {
    return '/User';
  }
}
