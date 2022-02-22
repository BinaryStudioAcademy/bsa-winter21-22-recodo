import { User } from '../user/user';
import { CommentReactionDTO } from '../reaction/comment-reaction';

export interface Comment {
  id: number;
  createdAt: Date;
  videoID: number;
  author: User;
  body: string;
  reactions: CommentReactionDTO[];
}
