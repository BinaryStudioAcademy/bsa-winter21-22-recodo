import { Injectable, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { ShareDialogComponent } from '../modules/workspace/share-dialog/share-dialog.component';

@Injectable({ providedIn: 'root' })
export class SendDialogService implements OnDestroy {
  private unsubscribe$ = new Subject<void>();

  public constructor(private dialog: MatDialog) {}

  public openSendDialog(link: string) {
    this.dialog.open(ShareDialogComponent, {
      data: { link },
      minWidth: 300,
      autoFocus: true,
      backdropClass: 'dialog-backdrop',
      position: {
        top: '500',
      },
    });
  }

  public ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
