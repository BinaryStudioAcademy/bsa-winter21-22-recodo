import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { VideoDTO } from '../models/video/video-dto';
import { ResourceService } from './resource.service';

@Injectable({ providedIn: 'root' })
export class VideoService extends ResourceService<VideoDTO> {
  getResourceUrl(): string {
    return '/video';
  }

  constructor(override httpClient: HttpClient, private router: Router) {
    super(httpClient);
  }

  public getAllVideos() {
    return this.getList();
  }

  public getVideoById(videoId: number) {
    return this.get(videoId);
  }
}
