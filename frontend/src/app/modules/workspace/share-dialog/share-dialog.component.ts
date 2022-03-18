/* eslint-disable @typescript-eslint/no-explicit-any */
import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { MailService } from 'src/app/services/mail.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  templateUrl: './share-dialog.component.html',
  styleUrls: ['./share-dialog.component.scss'],
})
export class ShareDialogComponent implements OnInit, OnDestroy {
  public email = {} as string;
  public link = {} as string;
  public title: string;

  private unsubscribe$ = new Subject<void>();

  constructor(
    private dialogRef: MatDialogRef<ShareDialogComponent>,
    private matDialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private mailService: MailService,
    private toastr: ToastrService
  ) {
    this.title = 'Write the email of the person you want to send the post to';
    this.email = 'Write email';
  }

  public ngOnInit() {
    this.link = this.data.link;
  }

  public ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public close() {
    this.dialogRef.close(false);
  }

  public sendLink() {
    this.mailService
      .sendLink({ email: this.email, link: this.link })
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(() => {
        this.matDialog.closeAll();
        this.toastr.success(`Link was successfully sended to ${this.email}!`);
      });
  }
}
