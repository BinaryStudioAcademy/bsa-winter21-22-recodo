import { Injectable, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { ShareDialogComponent } from '../modules/workspace/share-dialog/share-dialog.component';
import { SharePropertiesComponent } from '../modules/workspace/share-properties-dialog/share-properties.component';

@Injectable({ providedIn: 'root' })
export class SendDialogService implements OnDestroy {
  private unsubscribe$ = new Subject<void>();

  public constructor(private dialog: MatDialog) {}

  public openSendDialog(link: string, videoId: number) {
    this.dialog.open(ShareDialogComponent, {
      data: { link, videoId },
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

  public openSharePropertiesDialog(
    link: string,
    checked: boolean,
    videoId: number
  ) {
    this.dialog.open(SharePropertiesComponent, {
      data: { link, checked, videoId },
      minWidth: 400,
      autoFocus: true,
      backdropClass: 'dialog-backdrop',
      position: {
        top: '500',
      },
    });
  }
}
