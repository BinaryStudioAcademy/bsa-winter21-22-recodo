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
    return 'video/react';
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

  public reactVideo(
    currentVideo: VideoDTO,
    reactionType: ReactionType,
    currentUser: User
  ) {
    if (currentVideo != null) {
      const [hasReaction, hasSuchReaction] = this.checkHasReaction(
        currentVideo.reactions,
        currentUser,
        reactionType
      );
      if (hasReaction) {
        this.deleteReaction(currentUser, currentVideo);
        this.add(
          this.addNewReaction(currentUser.id, currentVideo.id, reactionType)
        );
      } else if (hasSuchReaction) {
        const deletedReaction = this.deleteReaction(
          currentUser,
          currentVideo
        );
        if (deletedReaction != null) {
          this.delete(deletedReaction);
        }
      } else {
        this.add(
          this.addNewReaction(currentUser.id, currentVideo.id, reactionType)
        );
      }
    }
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

  public deleteReaction(currentUser: User, currentVideo: VideoDTO) {
    const foundReaction = currentVideo.reactions.find(
      (reaction) =>
        reaction.userId === currentUser.id &&
        reaction.videoId === currentVideo.id
    );
    currentVideo.reactions.filter((reaction) => reaction != foundReaction);
    return foundReaction?.id;
  }
}
