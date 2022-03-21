import { Component, Input } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { TeamDto } from 'src/app/models/user/team-dto';
import { UserDto } from 'src/app/models/user/user-dto';
import { TeamInviteComponent } from '../team-invite/team-invite.component';
import { environment } from '../../../../environments/environment';
@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent {
  @Input() public user: UserDto = {} as UserDto;
  selectedCriteria: TeamDto | undefined;
  members: number | undefined;
  value: string = '';

  constructor(public dialog: MatDialog) {}

  ngOnChanges() {
    this.selectedCriteria = this.user.teams.filter(
      (t) => t.authorId === this.user.id
    )[0];

    this.value = this.selectedCriteria.name;
    this.initValues();
  }

  initValues() {
    this.members = this.selectedCriteria?.memberCount;
  }

  onChange(e: any) {
    this.selectedCriteria = this.user.teams.filter(
      (t) => t.name === e.value
    )[0];

    this.value = this.selectedCriteria.name;
    this.initValues();
  }

  showInvite() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.data = 'http://localhost:4200/inviteteam/' + this.user.email;

    this.dialog.open(TeamInviteComponent, dialogConfig);
  }
}
