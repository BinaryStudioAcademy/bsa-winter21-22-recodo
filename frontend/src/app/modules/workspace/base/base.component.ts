import { Component } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { UserDto } from 'src/app/models/user/user-dto';
import { CustomIconService } from 'src/app/services/custom-icon.service';
import { RegistrationService } from 'src/app/services/registration.service';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.scss']
})
export class BaseComponent {

  public currentUser: UserDto = {} as UserDto
  private unsubscribe$ = new Subject<void>();

  constructor(private customService:CustomIconService,
    private registrationService: RegistrationService) {
    this.customService.init();

    this.registrationService.getUser()
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe((user) => (this.currentUser = user));;
  }

}
