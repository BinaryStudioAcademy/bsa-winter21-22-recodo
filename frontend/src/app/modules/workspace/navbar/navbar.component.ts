import { Component, ViewChild } from '@angular/core';
import { MatMenuTrigger } from '@angular/material/menu';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent {
  @ViewChild(MatMenuTrigger)
  contextMenu?: MatMenuTrigger;

  contextMenuPosition = { x: '0px', y: '0px' };
  constructor() {}
  onContextMenu(event: MouseEvent) {
    event.preventDefault();
    this.contextMenuPosition.x = window.outerWidth - 50 + 'px';
    this.contextMenuPosition.y = 100 + 'px';
    this.contextMenu?.menu.focusFirstItem('mouse');
    this.contextMenu?.openMenu();
  }

  onLogOut() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  }
}
