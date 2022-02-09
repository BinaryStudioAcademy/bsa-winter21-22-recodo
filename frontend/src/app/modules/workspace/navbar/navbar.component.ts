import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  @Output() toogleSidebarEvent:EventEmitter<boolean> = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }

  public toggleSidebar(){
    this.toogleSidebarEvent.emit();
  }
}
