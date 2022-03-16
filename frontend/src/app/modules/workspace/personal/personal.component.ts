import { Component, OnInit, ViewChild } from '@angular/core';
import { map, Subject, takeUntil } from 'rxjs';
import { UserDto } from 'src/app/models/user/user-dto';
import { RegistrationService } from 'src/app/services/registration.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FolderDto } from 'src/app/models/folder/folder-dto';
import { NewFolderDto } from 'src/app/models/folder/new-folder-dto';
import { FolderService } from 'src/app/services/folder.service';
import { MatMenuTrigger } from '@angular/material/menu';
import { ActivatedRoute } from '@angular/router';
import { VideoDto } from 'src/app/models/video/video-dto';
import { VideoService } from 'src/app/services/video.service';
import { TimeService } from 'src/app/services/time.service';


@Component({
  selector: 'app-content',
  templateUrl: './personal.component.html',
  styleUrls: ['./personal.component.scss'],
})
export class PersonalComponent implements OnInit {
  public src = '../../assets/icons/test-user-logo.png';

  @ViewChild(MatMenuTrigger) menuTrigger: MatMenuTrigger = {} as MatMenuTrigger;

  public currentUser: UserDto = {} as UserDto;
  public avatarLink: string = '';
  public isFolderFormShow = false;
  public isFolderRouteActive = false;
  folderForm : FormGroup = {} as FormGroup;
  folder : FolderDto = {} as FolderDto;
  folders : FolderDto[] = [];
  selectedFolderName: string = '';
  selectedFolderId: number | undefined;

  public videos: VideoDto[] = [];

  private unsubscribe$ = new Subject<void>();

  displayedColumns: string[] = ['name', 'owner', 'details'];

  constructor(
    private registrationService: RegistrationService,
    private formBuilder: FormBuilder,
    private folderService: FolderService,
    private route: ActivatedRoute,
    private videoService: VideoService,
    private timeService: TimeService ) {
      route.params.pipe(map(p => p['id']))
      .subscribe(id=> this.selectedFolderId = id);
    }

  ngOnInit(): void {
    this.getAutorithedUser();
    this.validateForm();
  }

  private getFolders() {
    return this.folderService.getAllFoldersByUserId(this.currentUser.id).subscribe(
      (result) => {
        this.folders = result;
      }
    );
  }

  private getVideos(id: number) {
    return this.videoService.getAllVideosWithoutFolderByUserId(id).
        subscribe(res => this.videos = res)
  }

  private getAutorithedUser() {
    return this.registrationService
    .getUser()
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe((user) => {
      if(user.avatarLink === null) {
        user.avatarLink = '../../assets/icons/test-user-logo.png';
      }
      this.currentUser = user;
      this.getFolders();
      this.getVideos(user.id);
    });;
 }

  private validateForm() {
    this.folderForm = this.formBuilder.group({
      name: [, {
        validators: [
          Validators.required,
          Validators.maxLength(30),
        ],
        updateOn: 'change',
      }],
    });
  }

  createFolder() {
    let newfolder : NewFolderDto = {
      name: this.folderForm.controls['name'].value,
      authorId: this.currentUser.id,
      teamId: undefined
    }

    this.folderService.add(newfolder).subscribe((response) => {
      this.folder = response.body as FolderDto;
      this.isFolderFormShow = false;
      this.folderForm.setValue({ 'name': ''});
      this.getFolders();
    });
  }

  showNewFolderForm() {
    this.isFolderFormShow = true;
  }

  public folderClick(folder: FolderDto) {
    this.selectedFolderName = ' / '+folder.name;
    this.isFolderRouteActive = true;
  }

  public onMenuTriggered() {
    this.menuTrigger?.menu.focusFirstItem('mouse');
    this.menuTrigger?.openMenu();
  }

  public deleteFolder(id: number) {
    this.folderService.deleteFolder(id).subscribe(()=>{
      this.selectedFolderName = '';
      this.getFolders();
    });
  }

  public deleteVideo(id: number) {
    this.videoService.delete(id).subscribe(() => this.getVideos(this.currentUser.id));
  }

  public calculateTimeDifference(oldDate: Date) {
    return this.timeService.calculateTimeDifference(oldDate);
  }

}
