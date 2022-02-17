import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NewFolder } from 'src/app/models/folder/new-folder';

@Component({
  selector: 'app-content',
  templateUrl: './personal.component.html',
  styleUrls: ['./personal.component.scss'],
})
export class PersonalComponent {
  public src = '../../assets/icons/test-user-logo.png';
  public isFolderFormShow = false;
  folderForm : FormGroup = {} as FormGroup;
  folder : NewFolder = {} as NewFolder;

  //now i can`t get current user and his team cause not implementer this services
  //it's mock team and user id
  team : number = 1;
  user : number = 4;
  constructor(private formBuilder: FormBuilder) {
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
    this.isFolderFormShow = false;
    this.folder.name = this.folderForm.value['name'];
    this.folder.authorId = this.user;
    this.folder.teamId = this.team;
    this.folderForm.setValue({ 'name': ''});
  }

  showNewFolderForm() {
    this.isFolderFormShow = true;
  }
}
