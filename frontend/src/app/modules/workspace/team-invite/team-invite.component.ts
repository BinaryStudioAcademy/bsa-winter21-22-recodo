import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-team-invite',
  templateUrl: './team-invite.component.html',
  styleUrls: ['./team-invite.component.scss'],
})
export class TeamInviteComponent implements OnInit {
  url: string = '';

  constructor(
    private dialogRef: MatDialogRef<TeamInviteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string
  ) {
    this.url = data;
  }

  ngOnInit(): void {}

  close() {
    this.dialogRef.close();
  }
}
