import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { VideoService } from 'src/app/services/video.service';
import { AccessForLinkService } from 'src/app/services/access-for-video-link.service';
import { RegistrationService } from 'src/app/services/registration.service';

@Component({
  selector: 'app-shared-video-page',
  templateUrl: './shared-video-page.component.html',
  styleUrls: ['./shared-video-page.component.scss'],
})
export class SharedVideoPageComponent {
  public viewsNumber: number;
  public videoId: number;
  public checked: boolean = false;
  public isPrivate: boolean = false;
  public userId = {} as number;
  constructor(
    private activateRoute: ActivatedRoute,
    private snackBarService: SnackBarService,
    private videoService: VideoService,
    private accessForLinkService: AccessForLinkService,
    private registrationService: RegistrationService
  ) {
    this.viewsNumber = 10;
    this.registrationService.getUser().subscribe((resp) => {
      this.userId = resp.id;
    });
    this.videoId = activateRoute.snapshot.params['videoId'];
    this.videoService.getVideoById(this.videoId).subscribe((resp) => {
      if (resp.body) {
        this.isPrivate = resp.body.isPrivate;
      }
    });
    if (this.accessForLinkService.GetAccessedUser(this.videoId, this.userId) || this.isPrivate) {
      this.checked = true;
    }
  }
  public openSnackBar() {
    this.snackBarService.openSnackBar('Link was successfully copied!');
  }
}
