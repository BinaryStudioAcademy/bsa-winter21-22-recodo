import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-video-description',
  templateUrl: './video-description.component.html',
  styleUrls: ['./video-description.component.scss'],
})
export class VideoDescriptionComponent {
  @Input() public Username?: string;
  @Input() public DateOfComment?: string;
  public src = '../../assets/icons/test-user-logo.png';
  constructor() {}
}
