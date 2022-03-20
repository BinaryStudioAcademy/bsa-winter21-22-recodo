import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ReactionType } from '../models/common/reaction-type';
import { NewVideoReactionDTO } from '../models/reaction/new-video-reaction';
import { VideoReactionDTO } from '../models/reaction/video-reaction-dto';
import { User } from '../models/user/user';
import { VideoDTO } from '../models/video/video-dto';
import { ResourceService } from './resource.service';

@Injectable({
  providedIn: 'root',
})
export class VideoReactionService extends ResourceService<VideoDTO> {
  constructor(override httpClient: HttpClient, private router: Router) {
    super(httpClient);
  }

  getResourceUrl(): string {
    return '/video/react';
  }

  private checkHasReaction(
    reactions: VideoReactionDTO[],
    userId: number,
    reactionType: ReactionType
  ) {
    const hasReaction = reactions.some((x) => x.userId === userId);
    const hasSuchReaction = reactions.some(
      (x) => x.userId === userId && x.reaction === reactionType
    );
    return [hasReaction, hasSuchReaction];
  }

  public reactVideo(
    currentVideo: VideoDTO,
    reactionType: ReactionType,
    userId: number
  ) {
    if (currentVideo != null) {
      const [hasReaction, hasSuchReaction] = this.checkHasReaction(
        currentVideo.reactions,
        userId,
        reactionType
      );
      if (hasReaction) {
        const deletedReaction = this.deleteReaction(userId, currentVideo);
        if (deletedReaction != null) {
          this.delete(deletedReaction).subscribe();
        }
        this.add(
          this.addNewReaction(userId, currentVideo.id, reactionType)
        ).subscribe();
      } else if (hasSuchReaction) {
        const deletedReaction = this.deleteReaction(userId, currentVideo);
        if (deletedReaction != null) {
          this.delete(deletedReaction).subscribe();
        }
      } else {
        return this.add(
          this.addNewReaction(userId, currentVideo.id, reactionType)
        ).subscribe();
      }
    }
    return;
  }

  public addNewReaction(
    userId: number,
    videoId: number,
    reactionType: ReactionType
  ) {
    const newReaction: NewVideoReactionDTO = {
      videoId: videoId,
      userId: userId,
      reaction: reactionType,
    };
    return newReaction;
  }

  public deleteReaction(userId: number, currentVideo: VideoDTO) {
    const foundReaction = currentVideo.reactions.find(
      (reaction) =>
        reaction.userId === userId && reaction.videoId === currentVideo.id
    );
    currentVideo.reactions.filter((reaction) => reaction != foundReaction);
    return foundReaction?.id;
  }
}
