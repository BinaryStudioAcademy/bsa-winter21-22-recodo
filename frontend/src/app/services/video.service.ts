import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Comment } from '../models/comment/comment';
import { NewComment } from '../models/comment/new-comment';
import { VideoDTO } from '../models/video/video-dto';
import { ResourceService } from './resource.service';

@Injectable({ providedIn: 'root' })
export class VideoService extends ResourceService<VideoDTO> {
  getResourceUrl(): string {
    return '';
  }
  public routePrefix = '/api/video';

  constructor(override httpClient: HttpClient, private router: Router) {
    super(httpClient);
  }

  public getAllVideos() {
    return this.getFullRequest();
  }
}
