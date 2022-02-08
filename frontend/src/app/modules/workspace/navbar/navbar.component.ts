import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  @Output() toogleSidebarEvent:EventEmitter<any> = new EventEmitter();
  constructor(
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer) {
      this.matIconRegistry.addSvgIcon(
        "search",
        this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/Search.svg")
      );
      this.matIconRegistry.addSvgIcon(
        "bell",
        this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/Bell.svg")
      );
      this.matIconRegistry.addSvgIcon(
        "menu",
        this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/Menu.svg")
      );
     }

  ngOnInit(): void {
  }

  public toggleSidebar(){
    this.toogleSidebarEvent.emit();
  }
}
