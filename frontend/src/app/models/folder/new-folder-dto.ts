export interface NewFolderDto{
  name:string;
  authorId:number;
  parentId:number | undefined;
  teamId:number;
}