import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SendDialogService } from 'src/app/services/send-dialog.service';
import { environment } from 'src/environments/environment';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { VideoService } from 'src/app/services/video.service';

@Component({
  selector: 'app-video-page',
  templateUrl: './video-page.component.html',
  styleUrls: ['./video-page.component.scss'],
})
export class VideoPageComponent {
  public viewsNumber: number;
  public videoId: number;
  public link: string;
  public checked: boolean = false;
  constructor(
    private activateRoute: ActivatedRoute,
    private sendDialogService: SendDialogService,
    private snackBarService: SnackBarService,
    private videoService: VideoService
  ) {
    this.viewsNumber = 10;
    this.videoId = activateRoute.snapshot.params['videoId'];
    this.link = `${environment.appUrl}/shared/${this.videoId}`;
    this.videoService.getVideoById(this.videoId).subscribe((resp) => {
      if (resp.body) {
        this.checked = resp.body.isPrivate;
      }
    });
  }

  public openSendDialog() {
    this.sendDialogService.openSendDialog(this.link, this.videoId, this.checked);
  }

  public openSnackBar() {
    this.snackBarService.openSnackBar('Link was successfully copied!');
  }
}
