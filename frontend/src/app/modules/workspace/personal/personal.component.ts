import { Component } from '@angular/core';

@Component({
  selector: 'app-content',
  templateUrl: './personal.component.html',
  styleUrls: ['./personal.component.scss'],
})
export class PersonalComponent {
  public src = '../../assets/icons/test-user-logo.png';
  isGrid = true;
  constructor() {}
}
