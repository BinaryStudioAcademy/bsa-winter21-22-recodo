import { Component } from '@angular/core';
import { CustomIconService } from 'src/app/services/custom-icon.service';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.scss']
})
export class BaseComponent {

  constructor(private customService:CustomIconService) { 
    this.customService.init();
  }

}
