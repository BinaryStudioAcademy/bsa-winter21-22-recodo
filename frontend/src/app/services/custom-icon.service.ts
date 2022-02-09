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
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/Personal.svg")
    );
    this.matIconRegistry.addSvgIcon(
      "share",
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/Share.svg")
    );
    this.matIconRegistry.addSvgIcon(
      "settings",
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/Settings.svg")
    );
    this.matIconRegistry.addSvgIcon(
      "team",
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/Team.svg")
    );

    //Horizontalbar icons
    this.matIconRegistry.addSvgIcon(
      "search",
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/Search.svg")
    );
    this.matIconRegistry.addSvgIcon(
      "bell",
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/Bell.svg")
    );
    this.matIconRegistry.addSvgIcon(
      "menu",
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/Menu.svg")
    );

    //Personal page icons
    this.matIconRegistry.addSvgIcon(
      "details",
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/Details.svg")
    );
    this.matIconRegistry.addSvgIcon(
      "share-item",
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/ShareItem.svg")
    );
    this.matIconRegistry.addSvgIcon(
      "star",
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/Star.svg")
    );
  }
}
