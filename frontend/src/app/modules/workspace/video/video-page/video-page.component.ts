import { Component } from '@angular/core';
import { VideoDto } from 'src/app/models/video/video-dto';
import { CommentService } from 'src/app/services/comment.service';
import { NewComment } from 'src/app/models/comment/new-comment';
import { ActivatedRoute } from '@angular/router';
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
  public currentVideo: VideoDto;
  public newComment = {} as NewComment;
  public link: string;
  public checked: boolean = false;
  private unsubscribe$ = new Subject<void>();
  public isLoading = true;

  constructor(
    private commentService: CommentService,
    private videoService: VideoService,
    private activateRoute: ActivatedRoute,
    private snackBarService: SnackBarService,
    private registrationService: RegistrationService
  ) {
    this.viewsNumber = 10;
    this.videoId = activateRoute.snapshot.params['id'];
    this.updateVideo();
    this.videoService.getVideoById(this.videoId).subscribe((resp) => {
      if (resp.body) {
        this.checked = resp.body.isPrivate;
      }
    });
    this.registrationService
      .getUser()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((user) => {
        this.userWorkspaceName = user.workspaceName;
      });
    setTimeout(() => (this.isLoading = false), 1000);
  }

  public deleteComment(commentId: number) {
    this.commentService
      .deleteComment(commentId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(() => {
        if (this.currentVideo != null) {
          this.currentVideo.comments = this.currentVideo.comments.filter(
            (comment) => comment.id !== commentId
          );
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

  public openSnackBar() {
    this.snackBarService.openSnackBar('Link was successfully copied!');
  }
}
