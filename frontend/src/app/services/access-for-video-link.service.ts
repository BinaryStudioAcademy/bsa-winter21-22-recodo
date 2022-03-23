import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { AccessForRegisteredUsers } from '../models/access/access-for-registered-users';
import { VideoDto } from '../models/video/video-dto';
import { ResourceService } from './resource.service';

@Injectable({
  providedIn: 'root',
})
export class AccessForLinkService extends ResourceService<AccessForRegisteredUsers> {
  getResourceUrl(): string {
    return '/access';
  }
  constructor(override httpClient: HttpClient) {
    super(httpClient);
  }
  public GetAccessedUser(videoId: number, userId: number)
  {
    this.get();
  }
}
