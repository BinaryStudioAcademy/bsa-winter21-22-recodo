import { Component } from '@angular/core';
import { User } from 'src/app/models/user/user';
import { VideoDTO } from 'src/app/models/video/video-dto';
import { Comment } from 'src/app/models/comment/comment';
import { CommentService } from 'src/app/services/comment.service';
import { Subject, takeUntil } from 'rxjs';
import { NewComment } from 'src/app/models/comment/new-comment';
import { VideoService } from 'src/app/services/video.service';

@Component({
  selector: 'app-video-page',
  templateUrl: './video-page.component.html',
  styleUrls: ['./video-page.component.scss'],
})
export class VideoPageComponent {
  public viewsNumber: number;
  public currentVideo: VideoDTO;
  public currentUser: User;
  public newComment = {} as NewComment;
  private unsubscribe$ = new Subject<void>();
  constructor(
    private commentService: CommentService,
    private videoService: VideoService
  ) {
    this.viewsNumber = 10;
    this.videoService.getVideoById(1).subscribe((resp) => {
      if (resp.body != null) {
        this.currentVideo = resp.body;
      }
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
      this.newComment.authorId = this.currentUser.id;
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
}
