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
export class VideoReactionsComponent implements OnChanges {
  @Input() public video: VideoDTO;
  @Input() public user: User;
  public allReactions: VideoReactionDTO[];
  public unsubscribe$ = new Subject<void>();

  constructor(private reactionsService: VideoReactionService) {
    this.allReactions = this.video?.reactions;
    this.updateReactions();
  }

  public ngOnChanges() {
    this.updateReactions();
  }

  public addReaction(reactionNumber: number) {
    switch (reactionNumber) {
      case 1:
        this.reactionsService.reactVideo(
          this.video,
          ReactionType.Like,
          this.user
        );
        break;
      case 2:
        this.reactionsService.reactVideo(
          this.video,
          ReactionType.Dislike,
          this.user
        );
        break;
      case 3:
        this.reactionsService.reactVideo(
          this.video,
          ReactionType.Love,
          this.user
        );
        break;
      case 4:
        this.reactionsService.reactVideo(
          this.video,
          ReactionType.Fun,
          this.user
        );
        break;
      case 5:
        this.reactionsService.reactVideo(
          this.video,
          ReactionType.Astonishment,
          this.user
        );
        break;
      case 6:
        this.reactionsService.reactVideo(
          this.video,
          ReactionType.Magically,
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
      case 3:
        return this.allReactions.filter((x) => x.reaction == ReactionType.Love)
          .length;
      case 4:
        return this.allReactions.filter((x) => x.reaction == ReactionType.Fun)
          .length;
      case 5:
        return this.allReactions.filter(
          (x) => x.reaction == ReactionType.Astonishment
        ).length;
      case 6:
        return this.allReactions.filter(
          (x) => x.reaction == ReactionType.Magically
        ).length;
      default:
        return 0;
    }
  }

  private updateReactions() {
    this.allReactions = this.video?.reactions;
  }
}
