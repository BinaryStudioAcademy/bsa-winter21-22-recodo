import { Component, OnInit } from '@angular/core';
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  constructor(
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer) 
    {
      this.matIconRegistry.addSvgIcon(
        "personal",
        this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/Personal.svg")
      );
      this.matIconRegistry.addSvgIcon(
        "share",
        this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/Share.svg")
      );
      this.matIconRegistry.addSvgIcon(
        "settings",
        this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/Settings.svg")
      );
      this.matIconRegistry.addSvgIcon(
        "team",
        this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/Team.svg")
      );
    }

  ngOnInit(): void {
  }

}
