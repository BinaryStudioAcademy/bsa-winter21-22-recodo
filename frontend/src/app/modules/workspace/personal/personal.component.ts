import { Component, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { UserDto } from 'src/app/models/user/user-dto';
import { RegistrationService } from 'src/app/services/registration.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FolderDto } from 'src/app/models/folder/folder-dto';
import { NewFolderDto } from 'src/app/models/folder/new-folder-dto';
import { FolderService } from 'src/app/services/folder.service';


const ELEMENT_DATA = [
  { name: 'Screenshot name Screenshot name Screenshot name', owner: 'Volodymyr',parentId: undefined, teamId: 4 },
  { name: 'Screenshot name Screenshot name Screenshot name', owner: 'Volodymyr',parentId: undefined, teamId: 4 },
  { name: 'Screenshot name Screenshot name Screenshot name', owner: 'Volodymyr',parentId: undefined, teamId: 4 },
  { name: 'Screenshot name Screenshot name Screenshot name', owner: 'Volodymyr',parentId: undefined, teamId: 4 }
];

@Component({
  selector: 'app-content',
  templateUrl: './personal.component.html',
  styleUrls: ['./personal.component.scss'],
})
export class PersonalComponent implements OnInit {
  public src = '../../assets/icons/test-user-logo.png';

  public currentUser: UserDto = {} as UserDto;
  public isFolderFormShow = false;
  folderForm : FormGroup = {} as FormGroup;
  folder : FolderDto = {} as FolderDto;

  private unsubscribe$ = new Subject<void>();
 //now i can`t get current user and his team cause not implementer this services
  //it's mock team and user id
  team : number = 1;
  user : number = 4;
  currentFolder : number | undefined;

  displayedColumns: string[] = ['name', 'owner', 'details'];
  dataSource = ELEMENT_DATA;
  constructor(
    private registrationService: RegistrationService,
    private formBuilder: FormBuilder,
    private folderService: FolderService ) {}

  ngOnInit(): void {
    this.getAutorithedUser();
    this.validateForm();
  }

  private getAutorithedUser() {
    return this.registrationService
    .getUser()
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe((user) => (this.currentUser = user));;
 }

  private validateForm() {
    this.folderForm = this.formBuilder.group({
      name: [, {
        validators: [
          Validators.required
        ],
        updateOn: 'change',
      }],
    });
  }

  createFolder() {
    let newfolder : NewFolderDto = {
      name: this.folderForm.controls['name'].value,
      parentId: this.currentFolder,
      authorId: this.user,
      teamId: this.team
    }

    this.folderService.add(newfolder).subscribe((response) => {
      this.folder = response.body as FolderDto;
      this.isFolderFormShow = false;
      this.folderForm.setValue({ 'name': ''});
    });
  }

  showNewFolderForm() {
    this.isFolderFormShow = true;
  }
}
