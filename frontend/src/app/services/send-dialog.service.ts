import { Injectable, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { SharingVideoDialogComponent } from '../modules/workspace/sharing-propetries/sharing-video/sharing-video-dialog.component';

@Injectable({ providedIn: 'root' })
export class SendDialogService implements OnDestroy {
  private unsubscribe$ = new Subject<void>();

  public constructor(private dialog: MatDialog) {}

  public openSendDialog(link: string, videoId: number, checked: boolean) {
    this.dialog.open(SharingVideoDialogComponent, {
      data: { link, videoId, checked },
      minWidth: 600,
      autoFocus: true,
      backdropClass: 'dialog-backdrop',
    });
  }

  public ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
