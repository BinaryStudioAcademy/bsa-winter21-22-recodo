import { Component } from '@angular/core';
import { UserDto } from 'src/app/models/user/user-dto';
import { VideoDto } from 'src/app/models/video/video-dto';
import { Comment } from 'src/app/models/comment/comment';
import { CommentService } from 'src/app/services/comment.service';
import { Subject, takeUntil } from 'rxjs';
import { NewComment } from 'src/app/models/comment/new-comment';
import { VideoService } from 'src/app/services/video.service';
import { ActivatedRoute } from '@angular/router';
import { ReactionType } from 'src/app/models/common/reaction-type';

@Component({
  selector: 'app-video-page',
  templateUrl: './video-page.component.html',
  styleUrls: ['./video-page.component.scss'],
})
export class VideoPageComponent {
  public viewsNumber: number;
  public videoId: number;
  public currentVideo: VideoDto;
  public currentUser: UserDto;
  public newComment = {} as NewComment;
  private unsubscribe$ = new Subject<void>();
  constructor(
    private commentService: CommentService,
    private videoService: VideoService,
    private activateRoute: ActivatedRoute
  ) {
    this.viewsNumber = 10;
    this.videoId = activateRoute.snapshot.params['id'];
    this.updateVideo();
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
        this.userWorkspaceName = user.workspaceName;
      });
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

  public sendComment() {
    if (this.currentVideo != null) {
      this.newComment.authorId = this.currentVideo.authorId;
      this.newComment.videoId = this.currentVideo.id;
    }
    this.commentService
      .createComment(this.newComment)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((resp) => {
        if (resp && this.currentVideo != null) {
          this.currentVideo.comments = this.sortCommentArray(
            this.currentVideo.comments.concat(resp.body as Comment)
          );
          this.newComment.body = '';
        }
      });
  }
  private sortCommentArray(array: Comment[]): Comment[] {
    return array.sort(
      (a, b) => +new Date(b.createdAt) - +new Date(a.createdAt)
    );
  }

  public isCurrentVideo() {
    if (this.currentVideo) {
      return true;
    } else {
      return false;
    }
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
}
