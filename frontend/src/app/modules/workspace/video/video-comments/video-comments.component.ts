import { Component, Input } from '@angular/core';
import { Subject } from 'rxjs';
import { VideoReactionDTO } from 'src/app/models/reaction/video-reaction-dto';
import { User } from 'src/app/models/user/user';
import { CommentReactionService } from 'src/app/services/comment-reaction.service';
import { Comment } from 'src/app/models/comment/comment';

@Component({
  selector: 'app-video-comments',
  templateUrl: './video-comments.component.html',
  styleUrls: ['./video-comments.component.scss'],
})
export class VideoCommentsComponent {
  @Input() public comment: Comment;
  @Input() public currentUser: User;
  @Input() public deleteComment: (commentId: number) => void;
  @Input() public editComment: (commentId: number, commentBody: string) => void;

  public allReactions?: VideoReactionDTO[];
  public isEditingMode = false;
  private unsubscribe$ = new Subject<void>();

  constructor(private commentReactionService: CommentReactionService) {}
  public ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public toggleIsEditingMode() {
    this.isEditingMode = !this.isEditingMode;
  }

  public onDeleteComment() {
    if (this.deleteComment !== null) {
      this.deleteComment(this.comment.id);
    }
  }

  public onEditComment() {
    this.toggleIsEditingMode();
    this.editComment(this.comment.id, this.comment.body);
  }
}
