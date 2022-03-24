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
  private userId?: number;
  constructor(
    private activateRoute: ActivatedRoute,
    private snackBarService: SnackBarService,
    private videoService: VideoService,
    private accessForLinkService: AccessForLinkService,
    private registrationService: RegistrationService
  ) {
    this.viewsNumber = 10;
    this.videoId = parseInt(activateRoute.snapshot.params['videoId']);
    this.registrationService.getUser().subscribe((resp) => {
      localStorage.setItem('userId', resp.id.toString());
    });
    const userIdString = localStorage.getItem('userId');
    if (userIdString) {
      this.userId = parseInt(userIdString);
      localStorage.removeItem('userId');
    }
    this.videoService.getVideoById(this.videoId).subscribe((resp) => {
      if (resp.body) {
        if (resp.body.isPrivate) {
          localStorage.setItem('isPrivate', 'true');
        } else {
          localStorage.setItem('isPrivate', 'false');
        }
      }
    });
    if (localStorage.getItem('isPrivate') == 'true') {
      this.isPrivate = true;
    } else {
      this.isPrivate = false;
    }
    localStorage.removeItem('isPrivate');
    if (this.userId) {
      console.log(
        'het accessed user: ' +
          this.accessForLinkService.GetAccessedUser(this.videoId, this.userId)
      );
      if (
        this.accessForLinkService.GetAccessedUser(this.videoId, this.userId) ||
        this.isPrivate
      ) {
        this.checked = true;
      }
    }
  }

  public openSnackBar() {
    this.snackBarService.openSnackBar('Link was successfully copied!');
  }

  private getUserId(): Promise<number> {
    return new Promise((r) => {
      this.registrationService.getUser().subscribe((resp) => {
        return r(resp.id);
      });
    });
  }
}
