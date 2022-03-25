import { Comment } from '../comment/comment';
import { VideoReactionDTO } from '../reaction/video-reaction-dto';

export interface VideoDto {
  id: number;
  name: string;
  description: string;
  link: string;
  authorId: number;
  createdAt: Date;
  folderId: number;
  reactions: VideoReactionDTO[];
  comments: Comment[];
}
