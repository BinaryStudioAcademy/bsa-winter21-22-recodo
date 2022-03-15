import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { VideoDto } from '../models/video/video-dto';
import { ResourceService } from './resource.service';

@Injectable({
  providedIn: 'root'
})
export class VideoService extends ResourceService<VideoDto> {
  getResourceUrl(): string {
    return '/videos';
  }

  public getAllVideosByFolderId(id: number) {
    return this.getFullRequest<VideoDto>(`videos/${id}`).pipe(
      map((resp) => {
        return resp.body as unknown as VideoDto[];
      })
    );
  }

  public getAllVideosWithoutFolderByUserId(id: number) {
    return this.getFullRequest<VideoDto>(`videos/user/${id}`).pipe(
      map((resp) => {
        return resp.body as unknown as VideoDto[];
      })
    );
  }

  public deleteVideo(id: number) {
    return this.delete(id);
  }

  constructor(override httpClient: HttpClient) {
    super(httpClient);
   }
}
