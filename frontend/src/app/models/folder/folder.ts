import { UserDto } from '../user/user-dto';

export interface Folder{
  id:number;
  name:string;
  authorId:number;
  author:UserDto;
  parent:Folder;
  parentId:number;
  teamId:number;
  subFolders:Folder[];
}