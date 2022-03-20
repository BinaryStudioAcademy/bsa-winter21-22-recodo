import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Subject } from 'rxjs';
import { ReactionType } from 'src/app/models/common/reaction-type';
import { VideoReactionDTO } from 'src/app/models/reaction/video-reaction-dto';
import { VideoDTO } from 'src/app/models/video/video-dto';
import { VideoReactionService } from 'src/app/services/video-reactions.service';

@Component({
  selector: 'app-video-reactions',
  templateUrl: './video-reactions.component.html',
  styleUrls: ['./video-reactions.component.scss'],
})
export class VideoReactionsComponent {
  @Input() public video: VideoDTO;
  @Output() newReaction = new EventEmitter<boolean>();
  public allReactions: VideoReactionDTO[];
  public unsubscribe$ = new Subject<void>();

  constructor(private reactionsService: VideoReactionService) {
    this.allReactions = this.video?.reactions;
    this.updateReactions();
  }

  public addReaction(reactionNumber: number) {
    switch (reactionNumber) {
      case 1:
        this.reactionsService.reactVideo(
          this.video,
          ReactionType.Like,
          this.video.authorId
        );
        break;
      case 2:
        this.reactionsService.reactVideo(
          this.video,
          ReactionType.Dislike,
          this.video.authorId
        );
        break;
      case 3:
        this.reactionsService.reactVideo(
          this.video,
          ReactionType.Love,
          this.video.authorId
        );
        break;
      case 4:
        this.reactionsService.reactVideo(
          this.video,
          ReactionType.Fun,
          this.video.authorId
        );
        break;
      case 5:
        this.reactionsService.reactVideo(
          this.video,
          ReactionType.Astonishment,
          this.video.authorId
        );
        break;
      case 6:
        this.reactionsService.reactVideo(
          this.video,
          ReactionType.Magically,
          this.video.authorId
        );
        break;
      default:
        break;
    }
    this.updateReactions();
  }

  public GetReactions(reactionNumber: number) {
    if (!this.allReactions) {
      this.updateReactions();
    }
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
    this.newReaction.emit(true);
  }
}
