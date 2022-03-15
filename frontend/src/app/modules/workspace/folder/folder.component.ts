import { Component, Input, ViewChild } from '@angular/core';
import { MatMenuTrigger } from '@angular/material/menu';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs';
import { UserDto } from 'src/app/models/user/user-dto';
import { VideoDto } from 'src/app/models/video/video-dto';
import { VideoService } from 'src/app/services/video.service';

@Component({
  selector: 'app-folder',
  templateUrl: './folder.component.html',
  styleUrls: ['./folder.component.scss']
})
export class FolderComponent {

  @ViewChild(MatMenuTrigger) menuTrigger: MatMenuTrigger = {} as MatMenuTrigger;
  @Input() groupValue: string = 'grid';
  @Input() currentUser: UserDto = {} as UserDto

  public videos: VideoDto[] = [];
  public folderId: number = 0;
  displayedColumns: string[] = ['name', 'owner', 'details'];

  constructor(
    private route: ActivatedRoute,
    private videoService: VideoService) {
      route.params.pipe(map(p => p['id']))
      .subscribe(id => {
        this.getVideos(id);
        this.folderId = id;
      });
    }

  public calculateDiff(dateSent: Date) {
    let currentDate = new Date();
    dateSent = new Date(dateSent);

    return Math.floor((Date.UTC(currentDate.getFullYear(),
     currentDate.getMonth(),
     currentDate.getDate()) - Date.UTC(dateSent.getFullYear(),
     dateSent.getMonth(), dateSent.getDate()) ) /(1000 * 60 * 60 * 24));
  }

  public onMenuTriggered() {
    this.menuTrigger?.menu.focusFirstItem('mouse');
    this.menuTrigger?.openMenu();
  }

  public deleteVideo(id: number) {
    this.videoService.delete(id).subscribe(() => this.getVideos(this.folderId));
  }

  private getVideos(id: number) {
    return this.videoService.getAllVideosByFolderId(id).subscribe(res => this.videos = res)
  }

}