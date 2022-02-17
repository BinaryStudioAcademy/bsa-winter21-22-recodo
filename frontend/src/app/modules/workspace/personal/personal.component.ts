import { Component, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { UserDto } from 'src/app/models/user/user-dto';
import { RegistrationService } from 'src/app/services/registration.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-content',
  templateUrl: './personal.component.html',
  styleUrls: ['./personal.component.scss'],
})
export class PersonalComponent {
  public src = '../../assets/icons/test-user-logo.png';
  public currentUser: UserDto = {} as UserDto;
  private unsubscribe$ = new Subject<void>();
  constructor(private registrationService: RegistrationService) { 
    this.getAutorithedUser();
  }

  private getAutorithedUser() {
    return this.registrationService
    .getUser()
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe((user) => (this.currentUser = user));;
}
}
