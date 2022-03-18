import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SendDialogService } from 'src/app/services/send-dialog.service';
import { environment } from 'src/environments/environment';
import { SnackBarService } from 'src/app/services/snack-bar.service';

@Component({
  selector: 'app-video-page',
  templateUrl: './video-page.component.html',
  styleUrls: ['./video-page.component.scss'],
})
export class VideoPageComponent {
  public viewsNumber: number;
  public videoId: number;
  public link: string;
  constructor(
    private activateRoute: ActivatedRoute,
    private sendDialogService: SendDialogService,
    private snackBarService: SnackBarService
  ) {
    this.viewsNumber = 10;
    this.videoId = activateRoute.snapshot.params['videoId'];
    this.link = `${environment.appUrl}/video/${this.videoId}`;
  }

  public openSendDialog() {
    this.sendDialogService.openSendDialog(this.link);
  }

  public openSnackBar() {
    this.snackBarService.openSnackBar('Link was successfully copied!');
  }
}
