import { Component, Input} from '@angular/core';
import { UserDto } from 'src/app/models/user/user-dto';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  @Input() public user: UserDto = {} as UserDto
  public isUserPanelOpen = false;
  constructor() { }

  public toggleUserPersonalPanel() {
    this.isUserPanelOpen = !this.isUserPanelOpen
  }
}
