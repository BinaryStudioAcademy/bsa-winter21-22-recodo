import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FolderDto } from 'src/app/models/folder/folder-dto';
import { NewFolderDto } from 'src/app/models/folder/new-folder-dto';
import { FolderService } from 'src/app/services/folder.service';

@Component({
  selector: 'app-content',
  templateUrl: './personal.component.html',
  styleUrls: ['./personal.component.scss'],
})
export class PersonalComponent implements OnInit {
  public src = '../../assets/icons/test-user-logo.png';
  public isFolderFormShow = false;
  folderForm : FormGroup = {} as FormGroup;
  folder : FolderDto = {} as FolderDto;

  //now i can`t get current user and his team cause not implementer this services
  //it's mock team and user id
  team : number = 1;
  user : number = 4;
  currentFolder : number | undefined;
  constructor(private formBuilder: FormBuilder, private folderService: FolderService)
  {}

  ngOnInit(): void {
    this.validateForm();
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
