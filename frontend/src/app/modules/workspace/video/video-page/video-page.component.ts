import { Component } from '@angular/core';
import { User } from 'src/app/models/user/user';
import { VideoDTO } from 'src/app/models/video/video-dto';
import { ReactionType } from 'src/app/models/common/reaction-type';
import { Comment } from 'src/app/models/comment/comment';
import { CommentService } from 'src/app/services/comment.service';
import { Subject, takeUntil } from 'rxjs';
import { NewComment } from 'src/app/models/comment/new-comment';

@Component({
  selector: 'app-video-page',
  templateUrl: './video-page.component.html',
  styleUrls: ['./video-page.component.scss'],
})
export class VideoPageComponent {
  public testComment: Comment;
  public viewsNumber: number;
  public currentVideo: VideoDTO;
  public currentUser: User;
  public newComment = {} as NewComment;
  private unsubscribe$ = new Subject<void>();
  constructor(private commentService: CommentService) {
    this.viewsNumber = 10;
    this.currentUser = {
      id: 1,
      email: 'test.email@gmail.com',
      workspaceName: 'Test workspace',
      avatarLink: '',
    };
    this.currentVideo = {
      id: 1,
      name: 'Test video',
      description: 'Test description',
      link: 'Test link',
      authorId: 1,
      createdAt: new Date('2022-02-20'),
      folderId: 1,
      reactions: [
        {
          id: 1,
          userId: 1,
          videoId: 1,
          reaction: ReactionType.Like,
        },
      ],
      comments: [
        {
          id: 1,
          createdAt: new Date('2022-02-20'),
          videoID: 1,
          author: this.currentUser,
          body: 'test comment',
          reactions: [
            {
              id: 1,
              userId: 1,
              commentId: 1,
              reaction: ReactionType.Like,
            },
          ],
        },
        {
          id: 2,
          createdAt: new Date('2022-02-21'),
          videoID: 1,
          author: this.currentUser,
          body: 'test comment 2',
          reactions: [
            {
              id: 2,
              userId: 1,
              commentId: 2,
              reaction: ReactionType.Dislike,
            },
          ],
        },
      ],
    };
  }

  public editComment(commentId: number, commentBody: string) {
    const commentToEdit = this.currentVideo.comments.find(
      (comment) => comment.id === commentId
    );
    if (!commentToEdit) {
      return;
    }
    this.commentService
      .editComment({ ...commentToEdit, body: commentBody })
      .pipe(takeUntil(this.unsubscribe$));
  }

  public deleteComment(commentId: number) {
    this.commentService
      .deleteComment(commentId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(() => {
        this.currentVideo.comments = this.currentVideo.comments.filter(
          (comment) => comment.id !== commentId
        );
      });
  }

  public sendComment() {
    this.newComment.authorId = this.currentUser.id;
    this.newComment.videoId = this.currentVideo.id;

    this.commentService
      .createComment(this.newComment)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((resp) => {
        if (resp) {
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
