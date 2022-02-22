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
  @Input() public video: VideoDTO;
  @Input() public user: User;
  public allReactions: VideoReactionDTO[];
  public unsubscribe$ = new Subject<void>();

  constructor(private reactionsService: VideoReactionService) {
    this.reactionsService
      .GetCurrentVideo()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: (response: any) => {
          this.video = response.body;
        },
      });
    this.allReactions = this.video?.reactions;
    this.updateReactions();
  }

  public ngOnChanges() {
    this.updateReactions();
  }

  public addReaction(reactionType: ReactionType) {
    if (this.user != null && this.video != null) {
      this.reactionsService.reactVideo(this.video.id, reactionType, this.user);
    }
    this.updateReactions();
  }

  public GetReactions(reactionType: ReactionType) {
    const reactions: VideoReactionDTO[] = this.allReactions.filter(
      (x) => x.reaction == reactionType
    );
    return reactions;
  }

  private updateReactions() {
    this.allReactions = this.video.reactions;
  }
}
