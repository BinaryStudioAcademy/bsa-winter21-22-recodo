import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { ISendLink } from '../models/mail/send-link';
import { VideoDto } from '../models/video/video-dto';
import { ResourceService } from './resource.service';

@Injectable({
  providedIn: 'root',
})
export class VideoService extends ResourceService<VideoDto> {
  getResourceUrl(): string {
    return '/video';
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

  public sendLink(sendLinkInfo: ISendLink) {
    return this.addWithUrl<ISendLink, ISendLink>('share', sendLinkInfo);
  }

  constructor(override httpClient: HttpClient) {
    super(httpClient);
  }
}