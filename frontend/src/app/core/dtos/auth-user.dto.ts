import { IUserDTO } from './user.dto';

export interface IAuthUserDTO {
  user: IUserDTO;
  token: string;
}
