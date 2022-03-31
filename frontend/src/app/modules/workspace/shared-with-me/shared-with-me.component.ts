import { Component, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { FolderDto } from 'src/app/models/folder/folder-dto';
import { UserDto } from 'src/app/models/user/user-dto';
import { FolderService } from 'src/app/services/folder.service';
import { RegistrationService } from 'src/app/services/registration.service';

@Component({
  selector: 'app-shared-with-me',
  templateUrl: './shared-with-me.component.html',
  styleUrls: ['./shared-with-me.component.scss'],
})
export class SharedWithMeComponent implements OnInit {
  public folder: FolderDto;
  public currentUser: UserDto;
  private unsubscribe$ = new Subject<void>();

  constructor(
    private folderService: FolderService,
    private registrationService: RegistrationService
  ) {}
  ngOnInit() {
    this.getAutorithedUser();
  }

  private getAutorithedUser() {
    return this.registrationService
      .getUser()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((user) => {
        if (user.avatarLink === null) {
          user.avatarLink = '../../assets/icons/test-user-logo.png';
        }
        this.currentUser = user;
        this.getFolders();
      });
  }

  private getFolders() {
    return this.folderService
      .getAllFoldersByUserId(this.currentUser.id)
      .subscribe((result) => {
        const sharedWithMeFolder = result.find(
          (f) => f.name == 'Shared with me'
        );
        if (sharedWithMeFolder != null) {
          this.folder = sharedWithMeFolder;
        }
      });
  }
}
