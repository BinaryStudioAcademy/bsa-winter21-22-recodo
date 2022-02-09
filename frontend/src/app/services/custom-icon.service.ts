import { Injectable } from '@angular/core';
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";

@Injectable({
  providedIn: 'root'
})
export class CustomIconService {

  constructor(
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer
  ) { }

  public init(){
    //Sidebar icons
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

    //Horizontalbar icons
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

    //Personal page icons
    this.matIconRegistry.addSvgIcon(
      "details",
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/Details.svg")
    );
    this.matIconRegistry.addSvgIcon(
      "share-item",
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/ShareItem.svg")
    );
    this.matIconRegistry.addSvgIcon(
      "star",
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/Star.svg")
    );
  }
}
