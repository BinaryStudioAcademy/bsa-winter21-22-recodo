import { Injectable } from '@angular/core';
import { VideoDto } from '../models/video/video-dto';
import { ISendLink } from '../models/mail/send-link';
import { ResourceService } from './resource.service';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class MailService extends ResourceService<VideoDto> {
  getResourceUrl(): string {
    return '/mail';
  }

  constructor(override httpClient: HttpClient) {
    super(httpClient);
  }
  public sendLink(sendLinkInfo: ISendLink) {
    return this.add<ISendLink, ISendLink>(sendLinkInfo);
  }
}
