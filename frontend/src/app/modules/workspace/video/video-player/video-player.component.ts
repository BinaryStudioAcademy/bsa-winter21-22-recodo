import { HttpClient, HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs';
import { FileDto } from 'src/app/models/file/file-dto';
import { VideoUrlService } from 'src/app/services/video-url.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-video-player',
  templateUrl: './video-player.component.html',
  styleUrls: ['./video-player.component.scss'],
})
export class VideoPlayerComponent {
  private videoId: number = 0;
  private readonly blobApiUrl = environment.blobApiUrl;
  public videoUrl: string = '';
  public fileDto: FileDto = {} as FileDto;

  constructor(
    private route: ActivatedRoute,
    private videoUrlService: VideoUrlService,
    protected httpClient: HttpClient) {
    route.params.pipe(map(p => p['id']))
      .subscribe(id => {
        this.videoId = id;

        this.getVideoUrl().subscribe((result) => {
          if(result !== null) {
            this.videoUrl = result.url;
          }
        })
      });
  }

  public getVideoUrl() {
    const params = new HttpParams()
    .set('id', this.videoId);

    return this.videoUrlService.getFullRequest<FileDto>(`${this.blobApiUrl}/Blob/GetUrl`, params).pipe(
      map((resp) => {
        return resp.body;
      })
    );

  }
}
