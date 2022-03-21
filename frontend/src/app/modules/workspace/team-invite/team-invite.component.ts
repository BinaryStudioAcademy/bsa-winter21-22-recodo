import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-team-invite',
  templateUrl: './team-invite.component.html',
  styleUrls: ['./team-invite.component.scss'],
})
export class TeamInviteComponent {
  url: string = '';

  constructor(
    private dialogRef: MatDialogRef<TeamInviteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string
  ) {
    this.url = data;
  }

  close() {
    this.dialogRef.close();
  }
}
