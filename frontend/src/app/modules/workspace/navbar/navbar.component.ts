import { Component, ViewChild } from '@angular/core';
import { MatMenuTrigger } from '@angular/material/menu';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent {
  @ViewChild(MatMenuTrigger)
  contextMenu?: MatMenuTrigger;
  constructor(private loginService: LoginService) {}
  onContextMenu(event: MouseEvent) {
    event.preventDefault();
    this.contextMenu?.menu.focusFirstItem('mouse');
    this.contextMenu?.openMenu();
  }

  onLogOut() {
    this.loginService.logOut();
  }
}
