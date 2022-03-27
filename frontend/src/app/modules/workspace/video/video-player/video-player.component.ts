import { HttpClient, HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map, Subscription, timer } from 'rxjs';
import { FileDto } from 'src/app/models/file/file-dto';
import { VideoUrlService } from 'src/app/services/video-url.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-video-player',
  templateUrl: './video-player.component.html',
  styleUrls: ['./video-player.component.scss'],
})
export class VideoPlayerComponent {
  private readonly blobApiUrl = environment.blobApiUrl;
  private readonly mainApi = environment.apiUrl;

  private videoId: number = 0;
  public videoUrl: string = '';
  public fileDto: FileDto = {} as FileDto;
  private timerSubscription: Subscription = {} as Subscription;
  public isVideoSaved: boolean = false;
  private requestsCount: number = 0;
  constructor(
    private route: ActivatedRoute,
    private videoUrlService: VideoUrlService,
    protected httpClient: HttpClient
  ) {
    route.params.pipe(map((p) => p['id'])).subscribe((id) => {
      this.videoId = id;
      this.getVideoUrl();
    });
  }

  public getVideoUrl() {
    return (this.timerSubscription = timer(0, 5000)
      .pipe(
        map(() => {
          //Check how many  requests have been sent
          this.requestsCount++;
          if (this.requestsCount > 12) {
            this.timerSubscription.unsubscribe();
          }

          this.checkVideoState().subscribe((response) => {
            //Check if video is saved
            if (response) {
              //Get video url
              this.loadData().subscribe((response) => {
                if (response.status === 200 && response.body !== null) {
                  this.videoUrl = response.body.url;
                  this.timerSubscription.unsubscribe();
                  this.isVideoSaved = true;
                }
              });
            }
          });
        })
      )
      .subscribe());
  }

  private loadData() {
    const params = new HttpParams().set('id', this.videoId);

    return this.videoUrlService
      .getFullRequest<FileDto>(`${this.blobApiUrl}/Blob/GetUrl`, params)
      .pipe(
        map((resp) => {
          return resp;
        })
      );
  }

  private checkVideoState() {
    return this.videoUrlService
      .getFullRequest<boolean>(`${this.mainApi}/video/check/${this.videoId}`)
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }
}
