import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { Comment } from '../models/comment/comment';
import { ReactionType } from '../models/common/reaction-type';
import { CommentReactionDTO } from '../models/reaction/comment-reaction';
import { User } from '../models/user/user';
import { ResourceService } from './resource.service';

@Injectable({
  providedIn: 'root',
})
export class CommentReactionService extends ResourceService<Comment> {
  private currentComment: Comment;
  private unsubscribe$ = new Subject<void>();

  constructor(override httpClient: HttpClient, private router: Router) {
    super(httpClient);
    this.GetCurrentComment()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: (response: any) => {
          this.currentComment = response.body;
        },
      });
  }

  getResourceUrl(): string {
    return '';
  }

  private checkHasReaction(
    reactions: CommentReactionDTO[],
    currentUser: User,
    reactionType: ReactionType
  ) {
    const hasReaction = reactions.some((x) => x.userId === currentUser.id);
    const hasSuchReaction = reactions.some(
      (x) => x.userId === currentUser.id && x.reaction === reactionType
    );
    return [hasReaction, hasSuchReaction];
  }

  public GetCurrentComment() {
    return this.getFullRequest();
  }

  public reactVideo(
    videoId: number,
    reactionType: ReactionType,
    currentUser: User
  ) {
    if (this.currentComment != null) {
      const [hasReaction, hasSuchReaction] = this.checkHasReaction(
        this.currentComment.reactions,
        currentUser,
        reactionType
      );
      if (hasReaction) {
        this.deleteReaction(currentUser, videoId);
        this.currentComment.reactions.push(
          this.addNewReaction(currentUser.id, videoId, reactionType)
        );
      } else if (hasSuchReaction) {
        this.deleteReaction(currentUser, videoId);
      } else {
        this.currentComment.reactions.push(
          this.addNewReaction(currentUser.id, videoId, reactionType)
        );
      }
      this.updateReactions();
    }
  }

  public addNewReaction(
    userId: number,
    videoId: number,
    reactionType: ReactionType
  ) {
    const newReaction: CommentReactionDTO = {
      commentId: videoId,
      userId: userId,
      reaction: reactionType,
    };
    return newReaction;
  }

  public deleteReaction(currentUser: User, commentId: number) {
    if (this.currentComment != null) {
      const foundReaction = this.currentComment.reactions.find(
        (reaction) =>
          reaction.userId === currentUser.id && reaction.commentId === commentId
      );
      this.currentComment.reactions.filter(
        (reaction) => reaction != foundReaction
      );
    }
  }

  public updateReactions() {
    this.update(this.currentComment);
  }
}
