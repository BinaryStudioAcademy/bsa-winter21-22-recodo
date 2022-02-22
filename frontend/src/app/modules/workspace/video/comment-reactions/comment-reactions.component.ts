import { Component, Input, OnChanges } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { Comment } from 'src/app/models/comment/comment';
import { ReactionType } from 'src/app/models/common/reaction-type';
import { CommentReactionDTO } from 'src/app/models/reaction/comment-reaction';
import { VideoReactionDTO } from 'src/app/models/reaction/video-reaction-dto';
import { User } from 'src/app/models/user/user';
import { CommentReactionService } from 'src/app/services/comment-reaction.service';
import { VideoReactionService } from 'src/app/services/video-reactions.service';

@Component({
  selector: 'app-comment-reactions',
  templateUrl: './comment-reactions.component.html',
  styleUrls: ['./comment-reactions.component.scss'],
})
export class CommentReactionsComponent {
  @Input() public comment: Comment;
  @Input() public user: User;
  public allReactions: CommentReactionDTO[];
  public unsubscribe$ = new Subject<void>();

  constructor(private reactionsService: CommentReactionService) {
    this.reactionsService
      .GetCurrentComment()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: (response: any) => {
          this.comment = response.body;
        },
      });
    this.allReactions = this.comment.reactions;
    this.updateReactions();
  }

  public ngOnChanges() {
    this.updateReactions();
  }

  public addReaction(reactionType: ReactionType) {
    if (this.user != null && this.comment != null) {
      this.reactionsService.reactVideo(
        this.comment.id,
        reactionType,
        this.user
      );
    }
    this.updateReactions();
  }

  public GetReactions(reactionType: ReactionType) {
    const reactions: CommentReactionDTO[] = this.allReactions.filter(
      (x) => x.reaction == reactionType
    );
    return reactions;
  }

  private updateReactions() {
    this.allReactions = this.comment.reactions;
  }
}
