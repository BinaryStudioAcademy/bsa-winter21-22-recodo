import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AccessForRegisteredUsers } from '../models/access/access-for-registered-users';
import { ResourceService } from './resource.service';

@Injectable({
  providedIn: 'root',
})
export class AccessForLinkService extends ResourceService<AccessForRegisteredUsers> {
  private isAccessed: boolean = false;
  getResourceUrl(): string {
    return '/access';
  }
  constructor(override httpClient: HttpClient) {
    super(httpClient);
  }
  public GetAccessedUser(videoId: number, userId: number) {
    const accessRegisteredUser = {
      userId: userId,
      videoId: videoId,
    } as AccessForRegisteredUsers;
    this.add(accessRegisteredUser).subscribe((resp) => {
      if (resp.body != null) {
        localStorage.setItem('isAccessed', 'true');
      } else {
        localStorage.setItem('isAccessed', 'false');
      }
    });
    const isAccessed = localStorage.getItem('isAccessed');
    if (isAccessed == 'true') {
      this.isAccessed = true;
    } else {
      this.isAccessed = false;
    }
    localStorage.removeItem('isAccessed');
    return this.isAccessed;
  }
}
