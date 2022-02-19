import { VideoReactionDTO } from '../reaction/video-reaction-dto';

export interface VideoDTO {
  id: number;
  name: string;
  description: string;
  link: string;
  authorId: number;
  createdAt: Date;
  folderId: number;
  reactions: VideoReactionDTO[];
}
