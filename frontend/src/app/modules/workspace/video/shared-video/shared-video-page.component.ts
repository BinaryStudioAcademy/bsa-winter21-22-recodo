import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { VideoService } from 'src/app/services/video.service';
import { AccessForLinkService } from 'src/app/services/access-for-video-link.service';
import { RegistrationService } from 'src/app/services/registration.service';
import { Subject, takeUntil } from 'rxjs';
import { VideoDto } from 'src/app/models/video/video-dto';
import { NewComment } from 'src/app/models/comment/new-comment';
import { CommentService } from 'src/app/services/comment.service';
import { UserDto } from 'src/app/models/user/user-dto';

@Component({
  selector: 'app-shared-video-page',
  templateUrl: './shared-video-page.component.html',
  styleUrls: ['./shared-video-page.component.scss'],
})
export class SharedVideoPageComponent {
  @Input() user: UserDto;
  public isLoading = true;
  public viewsNumber: number;
  public videoId: number;
  public checked = true;
  public isPrivate?: boolean;
  private unsubscribe$ = new Subject<void>();
  public currentVideo: VideoDto;
  public newComment = {} as NewComment;

  constructor(
    private activateRoute: ActivatedRoute,
    private snackBarService: SnackBarService,
    private videoService: VideoService,
    private accessForLinkService: AccessForLinkService,
    private registrationService: RegistrationService,
    private commentService: CommentService,
  ) {
    this.viewsNumber = 1;
    this.videoId = parseInt(activateRoute.snapshot.params['id']);
    this.getVideo();
  }

  public getVideo() {
    this.videoService.getVideoById(this.videoId).subscribe((resp) => {
      if (resp.body) {
        this.isPrivate = resp.body.isPrivate;
        this.currentVideo = resp.body;
        this.checkAccess();
      }
    });
  }

  public checkAccess() {
    if (this.user.id) {
      this.accessForLinkService
        .CheckAccessedUser(this.videoId, this.user.id)
        .subscribe((resp) => {
          if (resp.body || this.isPrivate) {
            this.checked = false;
          }
          setTimeout(() => (this.isLoading = false), 1000);
        });
    }
  }

  public openSnackBar() {
    this.snackBarService.openSnackBar('Link was successfully copied!');
  }

  public updateVideo() {
    this.videoService
      .getVideoById(this.videoId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((resp) => {
        if (resp.body != null) {
          this.currentVideo = resp.body;
        }
      });
  }

  public sendComment(comment: NewComment) {
    if (this.currentVideo != null) {
      this.newComment.body = comment.body;
      this.newComment.authorId = this.currentVideo.authorId;
      this.newComment.videoId = this.currentVideo.id;
    }
    this.commentService
      .createComment(this.newComment)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((resp) => {
        if (resp && this.currentVideo != null) {
          this.newComment.body = '';
          this.updateVideo();
        }
      });
  }
}
