import { Component, Input, OnChanges } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { Comment } from 'src/app/models/comment/comment';
import { ReactionType } from 'src/app/models/common/reaction-type';
import { CommentReactionDTO } from 'src/app/models/reaction/comment-reaction';
import { VideoReactionDTO } from 'src/app/models/reaction/video-reaction-dto';
import { User } from 'src/app/models/user/user';
import { VideoDTO } from 'src/app/models/video/video-dto';
import { CommentReactionService } from 'src/app/services/comment-reaction.service';
import { VideoReactionService } from 'src/app/services/video-reactions.service';

@Component({
  selector: 'app-comment-reactions',
  templateUrl: './comment-reactions.component.html',
  styleUrls: ['./comment-reactions.component.scss'],
})
export class CommentReactionsComponent implements OnChanges {
  @Input() public comment: Comment;
  @Input() public user: User;
  public allReactions: CommentReactionDTO[];
  public unsubscribe$ = new Subject<void>();

  constructor(private reactionsService: CommentReactionService) {
    this.allReactions = this.comment?.reactions;
    this.updateReactions();
  }

  public ngOnChanges() {
    this.updateReactions();
  }

  public addReaction(reactionNumber: number) {
    switch (reactionNumber) {
      case 1:
        this.reactionsService.reactComment(
          this.comment,
          ReactionType.Like,
          this.user
        );
        break;
      case 2:
        this.reactionsService.reactComment(
          this.comment,
          ReactionType.Dislike,
          this.user
        );
        break;
      default:
        break;
    }
  }

  public GetReactions(reactionNumber: number) {
    switch (reactionNumber) {
      case 1:
        return this.allReactions.filter((x) => x.reaction == ReactionType.Like)
          .length;
      case 2:
        return this.allReactions.filter(
          (x) => x.reaction == ReactionType.Dislike
        ).length;
      default:
        return 0;
    }
  }

  private updateReactions() {
    this.allReactions = this.comment?.reactions;
  }
}
