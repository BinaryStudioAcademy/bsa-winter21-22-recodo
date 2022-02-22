import { ReactionType } from '../common/reaction-type';

export interface CommentReactionDTO {
  userId: number;
  commentId: number;
  reaction: ReactionType;
}
