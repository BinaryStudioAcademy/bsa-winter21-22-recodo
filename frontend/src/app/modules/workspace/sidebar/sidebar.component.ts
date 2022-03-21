import { Component, Input } from '@angular/core';
import { UserDto } from 'src/app/models/user/user-dto';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {

  @Input() public user: UserDto = {} as UserDto

  constructor() { }

}
