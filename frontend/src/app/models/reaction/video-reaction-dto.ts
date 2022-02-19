import { ReactionType } from '../common/reaction-type';

export interface VideoReactionDTO {
  userId: number;
  videoId: number;
  reaction: ReactionType;
}
