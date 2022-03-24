import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { VideoDTO } from '../models/video/video-dto';
import { ResourceService } from './resource.service';
import { map } from 'rxjs';

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

  public getAllVideosByFolderId(id: number) {
    return this.getFullRequest<VideoDTO>(`video/${id}`).pipe(
      map((resp) => {
        return resp.body as unknown as VideoDTO[];
      })
    );
  }

  public getAllVideosWithoutFolderByUserId(id: number) {
    return this.getFullRequest<VideoDTO>(`video/user/${id}`).pipe(
      map((resp) => {
        return resp.body as unknown as VideoDTO[];
      })
    );
  }

  public getVideoById(videoId: number) {
    return this.get(videoId);
  }

  public deleteVideo(id: number) {
    return this.delete(id);
  }

  public updateVideo(video: VideoDTO) {
    this.update(video);
  }
}
