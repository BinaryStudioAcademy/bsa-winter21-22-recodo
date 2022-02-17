import { Injectable } from "@angular/core";
import { Subject, takeUntil } from "rxjs";
import { UserDto } from "../models/user/user-dto";
import { RegistrationService } from "./registration.service";

@Injectable({
    providedIn: 'root',
  })

export class UserService {
    private currentUser: UserDto = {} as UserDto;
    private unsubscribe$ = new Subject<void>();

    constructor(private registrationservice: RegistrationService) {}

    public getAutorithedUser() {
        return this.registrationservice
        .getUser()
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe((user) => (this.currentUser = user));;
    }
}