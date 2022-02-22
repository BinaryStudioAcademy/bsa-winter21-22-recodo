import { UserDto } from '../user/user-dto';

export interface FolderDto{
  id:number;
  name:string;
  authorId:number;
  author:UserDto;
  parent:FolderDto;
  parentId:number;
  teamId:number;
  subFolders:FolderDto[];
}