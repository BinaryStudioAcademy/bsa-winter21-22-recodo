import {Component, Inject} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

export interface DialogData {
  name: string;
}

@Component({
  selector: 'app-folder-dialog',
  templateUrl: 'folder-dialog.component.html',
  styleUrls: ['folder-dialog.component.scss'],
})
export class FolderDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<FolderDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}