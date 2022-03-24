import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SendDialogService } from 'src/app/services/send-dialog.service';
import { environment } from 'src/environments/environment';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { VideoService } from 'src/app/services/video.service';
import { RegistrationService } from 'src/app/services/registration.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-video-page',
  templateUrl: './video-page.component.html',
  styleUrls: ['./video-page.component.scss'],
})
export class VideoPageComponent {
  public userWorkspaceName = {} as string;
  public viewsNumber: number;
  public videoId: number;
  public link: string;
  public checked: boolean = false;
  private unsubscribe$ = new Subject<void>();

  constructor(
    private activateRoute: ActivatedRoute,
    private sendDialogService: SendDialogService,
    private snackBarService: SnackBarService,
    private videoService: VideoService,
    private registrationService: RegistrationService
  ) {
    this.viewsNumber = 10;
    this.videoId = activateRoute.snapshot.params['videoId'];
    this.link = `${environment.appUrl}/shared/${this.videoId}`;
    this.videoService.getVideoById(this.videoId).subscribe((resp) => {
      if (resp.body) {
        this.checked = resp.body.isPrivate;
      }
    });
    this.registrationService
      .getUser()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((user) => {
        localStorage.setItem('workspaceName', user.workspaceName);
      });
    const workspaceName = localStorage.getItem('workspaceName');
    if (workspaceName) {
      this.userWorkspaceName = workspaceName;
    }
    localStorage.removeItem('workspaceName');
  }

  public openSendDialog() {
    this.sendDialogService.openSendDialog(
      this.link,
      this.videoId,
      this.checked,
      this.userWorkspaceName
    );
  }

  public openSnackBar() {
    this.snackBarService.openSnackBar('Link was successfully copied!');
  }
}
