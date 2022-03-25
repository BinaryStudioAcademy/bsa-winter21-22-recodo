import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { VideoDto } from '../models/video/video-dto';
import { ResourceService } from './resource.service';

@Injectable({ providedIn: 'root' })
export class VideoService extends ResourceService<VideoDto> {
  getResourceUrl(): string {
    return '/video';
  }

  constructor(override httpClient: HttpClient, private router: Router) {
    super(httpClient);
  }

  public getAllVideos() {
    return this.getList();
  }

  public getAllVideosByFolderId(id: number) {
    return this.getFullRequest<VideoDto>(`video/${id}`).pipe(
      map((resp) => {
        return resp.body as unknown as VideoDto[];
      })
    );
  }

  public getAllVideosWithoutFolderByUserId(id: number) {
    return this.getFullRequest<VideoDto>(`video/user/${id}`).pipe(
      map((resp) => {
        return resp.body as unknown as VideoDto[];
      })
    );
  }

  public getVideoById(videoId: number) {
    return this.get(videoId);
  }

  public deleteVideo(id: number) {
    return this.delete(id);
  }

  public updateVideo(video: VideoDto) {
    this.update(video);
  }
}
