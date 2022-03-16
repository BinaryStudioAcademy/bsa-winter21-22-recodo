import {
  Component,
  Input,
  OnDestroy,
  EventEmitter,
  Output,
} from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { VideoReactionDTO } from 'src/app/models/reaction/video-reaction-dto';
import { User } from 'src/app/models/user/user';
import { CommentReactionService } from 'src/app/services/comment-reaction.service';
import { Comment } from 'src/app/models/comment/comment';
import { CommentService } from 'src/app/services/comment.service';

@Component({
  selector: 'app-video-comments',
  templateUrl: './video-comments.component.html',
  styleUrls: ['./video-comments.component.scss'],
})
export class VideoCommentsComponent implements OnDestroy {
  @Input() public comment: Comment;
  @Input() public currentUser: User;
  @Output() deletedComment = new EventEmitter<number>();

  public allReactions?: VideoReactionDTO[];
  public isEditingMode = false;
  private unsubscribe$ = new Subject<void>();

  constructor(
    private commentReactionService: CommentReactionService,
    private commentService: CommentService
  ) {}
  public ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public toggleIsEditingMode() {
    this.isEditingMode = !this.isEditingMode;
  }

  public onDeleteComment() {
    this.deletedComment.emit(this.comment.id);
  }

  public onEditComment() {
    this.commentService
      .editComment({ ...this.comment, body: this.comment.body })
      .pipe(takeUntil(this.unsubscribe$));
  }
}
