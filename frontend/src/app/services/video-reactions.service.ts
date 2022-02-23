import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { ReactionType } from '../models/common/reaction-type';
import { VideoReactionDTO } from '../models/reaction/video-reaction-dto';
import { User } from '../models/user/user';
import { VideoDTO } from '../models/video/video-dto';
import { ResourceService } from './resource.service';

@Injectable({
  providedIn: 'root',
})
export class VideoReactionService extends ResourceService<VideoDTO> {
  private currentVideo?: VideoDTO;
  private unsubscribe$ = new Subject<void>();

  constructor(override httpClient: HttpClient, private router: Router) {
    super(httpClient);
    this.GetCurrentVideo()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: (response: any) => {
          this.currentVideo = response.body;
        },
      });
  }

  getResourceUrl(): string {
    return '';
  }

  private checkHasReaction(
    reactions: VideoReactionDTO[],
    currentUser: User,
    reactionType: ReactionType
  ) {
    const hasReaction = reactions.some((x) => x.userId === currentUser.id);
    const hasSuchReaction = reactions.some(
      (x) => x.userId === currentUser.id && x.reaction === reactionType
    );
    return [hasReaction, hasSuchReaction];
  }

  public GetCurrentVideo() {
    return this.getFullRequest();
  }

  public reactVideo(
    videoId: number,
    reactionType: ReactionType,
    currentUser: User
  ) {
    if (this.currentVideo != null) {
      const [hasReaction, hasSuchReaction] = this.checkHasReaction(
        this.currentVideo.reactions,
        currentUser,
        reactionType
      );
      if (hasReaction) {
        this.deleteReaction(currentUser, videoId);
        this.currentVideo.reactions.push(
          this.addNewReaction(currentUser.id, videoId, reactionType)
        );
      } else if (hasSuchReaction) {
        this.deleteReaction(currentUser, videoId);
      } else {
        this.currentVideo.reactions.push(
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
    const newReaction: VideoReactionDTO = {
      videoId: videoId,
      userId: userId,
      reaction: reactionType,
    };
    return newReaction;
  }

  public deleteReaction(currentUser: User, videoId: number) {
    if (this.currentVideo != null) {
      const foundReaction = this.currentVideo.reactions.find(
        (reaction) =>
          reaction.userId === currentUser.id && reaction.videoId === videoId
      );
      this.currentVideo.reactions.filter(
        (reaction) => reaction != foundReaction
      );
    }
  }

  public updateReactions() {
    this.update(this.currentVideo);
  }
}
