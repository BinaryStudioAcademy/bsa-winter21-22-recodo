import { Component, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-content',
  templateUrl: './content.component.html',
  styleUrls: ['./content.component.scss']
})
export class ContentComponent implements OnInit {

  constructor(
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer
  ) { 
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

  ngOnInit(): void {
  }

}
