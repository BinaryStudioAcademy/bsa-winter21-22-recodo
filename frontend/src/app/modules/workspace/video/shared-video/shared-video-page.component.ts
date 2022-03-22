import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { VideoService } from 'src/app/services/video.service';

@Component({
  selector: 'app-shared-video-page',
  templateUrl: './shared-video-page.component.html',
  styleUrls: ['./shared-video-page.component.scss'],
})
export class SharedVideoPageComponent {
  public viewsNumber: number;
  public videoId: number;
  public link: string;
  public checked: boolean = false;
  constructor(
    private activateRoute: ActivatedRoute,
    private snackBarService: SnackBarService,
    private videoService: VideoService
  ) {
    this.viewsNumber = 10;
    this.videoId = activateRoute.snapshot.params['videoId'];
    this.link = `${environment.appUrl}/personal/video/${this.videoId}`;
    this.videoService.getVideoById(this.videoId).subscribe((resp) => {
      if (resp.body) {
        this.checked = resp.body.isPrivate;
      }
    });
  }
  public openSnackBar() {
    this.snackBarService.openSnackBar('Link was successfully copied!');
  }
}
