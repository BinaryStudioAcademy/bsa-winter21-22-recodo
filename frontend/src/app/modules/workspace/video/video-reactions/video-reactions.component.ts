import { Component, Input, OnChanges } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { ReactionType } from 'src/app/models/common/reaction-type';
import { VideoReactionDTO } from 'src/app/models/reaction/video-reaction-dto';
import { User } from 'src/app/models/user/user';
import { VideoDTO } from 'src/app/models/video/video-dto';
import { VideoReactionService } from 'src/app/services/video-reactions.service';

@Component({
  selector: 'app-video-reactions',
  templateUrl: './video-reactions.component.html',
  styleUrls: ['./video-reactions.component.scss'],
})
export class VideoReactionsComponent {
  @Input() public currentVideo?: VideoDTO;
  @Input() public currentUser?: User;
  public allReactions?: VideoReactionDTO[];
  public Likes: VideoReactionDTO[] = [];
  public Dislikes?: VideoReactionDTO[];
  public LovesReactions?: VideoReactionDTO[];
  public MagicalReactions?: VideoReactionDTO[];
  public AstonishmentReactions?: VideoReactionDTO[];
  public FunReaction?: VideoReactionDTO[];
  public unsubscribe$ = new Subject<void>();

  constructor(private reactionsService: VideoReactionService) {
    this.reactionsService
      .GetCurrentVideo()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: (response: any) => {
          this.currentVideo = response.body;
        },
      });
    this.allReactions = this.currentVideo?.reactions;
    this.updateReactions();
  }

  public ngOnChanges() {
    this.updateReactions();
  }

  public addReaction(reactionType: ReactionType) {
    if (this.currentUser != null && this.currentVideo != null) {
      this.reactionsService.reactVideo(
        this.currentVideo.id,
        reactionType,
        this.currentUser
      );
    }
    this.updateReactions();
  }

  private updateReactions() {
    if (this.allReactions != null) {
      this.Likes = this.allReactions.filter(
        (x) => x.reaction === ReactionType.Like
      );
      this.Dislikes = this.allReactions.filter(
        (x) => x.reaction === ReactionType.Dislike
      );
      this.AstonishmentReactions = this.allReactions.filter(
        (x) => x.reaction === ReactionType.Astonishment
      );
      this.FunReaction = this.allReactions.filter(
        (x) => x.reaction === ReactionType.Fun
      );
      this.LovesReactions = this.allReactions.filter(
        (x) => x.reaction === ReactionType.Love
      );
      this.MagicalReactions = this.allReactions.filter(
        (x) => x.reaction === ReactionType.Magically
      );
    }
  }
}
