import { Component, Input, ViewChild } from '@angular/core';
import { MatMenuTrigger } from '@angular/material/menu';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs';
import { UserDto } from 'src/app/models/user/user-dto';
import { VideoDto } from 'src/app/models/video/video-dto';
import { TimeService } from 'src/app/services/time.service';
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
    private videoService: VideoService,
    private timeService: TimeService) {
      route.params.pipe(map(p => p['id']))
      .subscribe(id => {
        this.getVideos(id);
        this.folderId = id;
      });
    }

  public calculateTimeDifference(oldDate: Date) {
    return this.timeService.calculateTimeDifference(oldDate);
  }

  public onMenuTriggered() {
    this.menuTrigger?.menu.focusFirstItem('mouse');
    this.menuTrigger?.openMenu();
  }

  public deleteVideo(id: number) {
    if(confirm('Are you sure you want to delete the video ?'))
    {
      this.videoService.delete(id).subscribe(() => this.getVideos(this.folderId));
    }
  }

  private getVideos(id: number) {
    return this.videoService.getAllVideosByFolderId(id).subscribe(res => this.videos = res)
  }

}