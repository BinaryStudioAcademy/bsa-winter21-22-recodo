export interface VideoDto {
  id: number;
  name: string;
  description: string;
  link: string;
  authorId: number;
  folderId: number;
  createdAt: Date;
  isPrivate: boolean;
}
