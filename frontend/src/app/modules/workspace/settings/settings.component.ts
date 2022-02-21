import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import jwt_decode from 'jwt-decode';
import { UserService } from 'src/app/services/user.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})
export class SettingsComponent implements OnInit {
  private readonly APIUrl = environment.apiUrl;
  public settingsForm1: FormGroup = {} as FormGroup;
  public settingsForm2: FormGroup = {} as FormGroup;
  public avatar!: string;

  public oldName: string = '';
  public oldEmail: string = '';
  public userId: string = '';
  public name: string = '';
  public imageFile!: File;

  constructor(
    private formBuilder: FormBuilder,
    private httpService: HttpClient,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.initValues();
    this.initForms();
  }

  initValues() {
    let token = localStorage.getItem('accessToken') || '';

    if (token) {
      let decoded: { name: string; email: string; id: string } =
        jwt_decode(token);

      this.name = decoded.name;
      this.oldName = this.name;
      this.oldEmail = decoded.email;
      this.userId = decoded.id;
    }
  }

  initForms() {
    this.settingsForm1 = this.formBuilder.group({
      workspaceName: [
        this.name,
        {
          validators: [
            Validators.required,
            Validators.minLength(4),
            Validators.pattern('^[a-zA-Z0-9]+$'),
          ],
        },
      ],
    });

    this.settingsForm2 = this.formBuilder.group({
      email: [
        this.oldEmail,
        {
          validators: [Validators.required, Validators.email],
        },
      ],
      passOld: [
        ,
        {
          validators: [
            Validators.required,
            Validators.minLength(8),
            Validators.pattern('^[a-zA-Z0-9]+$'),
          ],
        },
      ],
      passNew: [
        ,
        {
          validators: [
            Validators.minLength(8),
            Validators.pattern('^[a-zA-Z0-9]$'),
          ],
        },
      ],
    });
  }

  errorHandler(event: any) {
    event.target.src = '../../assets/icons/test-user-logo.png';
  }

  saveNewInfoToDb() {}

  saveNewInfo() {
    let workspaceName = this.settingsForm1.controls['workspaceName'].value;

    const data = new FormData();
    data.append('id', this.userId);
    data.append('workspaceName', workspaceName);
    data.append('avatar', this.imageFile);

    this.userService.updateInfo('Update', data).subscribe({
      next: () => {
        window.alert('done');
      },
      error: () => {
        window.alert('error');
      },
    });
  }

  onSelect(event: { addedFiles: File[] }) {
    let added = event.addedFiles;
    if (added?.length > 0) {
      this.imageFile = added[0];

      const reader = new FileReader();
      reader.addEventListener(
        'load',
        () => (this.avatar = reader.result as string)
      );
      reader.readAsDataURL(this.imageFile);
    }
  }

  saveNewInfoCancel() {
    this.settingsForm1.controls['workspaceName'].setValue(this.oldName);
    this.avatar = '';
  }

  public handleFileInput(target: any) {
    this.imageFile = target.files[0];

    if (!this.imageFile) {
      target.value = '';
      return;
    }

    if (this.imageFile.size / 1000000 > 5) {
      window.alert("Image can't be heavier than ~5MB");
      target.value = '';
      return;
    }

    const reader = new FileReader();
    reader.addEventListener(
      'load',
      () => (this.avatar = reader.result as string)
    );
    reader.readAsDataURL(this.imageFile);
  }

  saveNewPass() {
    let email = this.settingsForm2.controls['email'].value;
    let passCurrent = this.settingsForm2.controls['passOld'].value;
    let passNew = this.settingsForm2.controls['passNew'].value;
    let userId = this.userId;

    this.httpService
      .post(`${this.APIUrl}/User/UpdatePassword`, {
        id: userId,
        email,
        PasswordCurrent: passCurrent,
        PasswordNew: passNew,
      })
      .subscribe({
        next: () => {
          window.alert('done');
        },
        error: () => {
          window.alert('error');
        },
      });
  }

  saveNewPassCancel() {
    this.settingsForm2.controls['email'].setValue(this.oldEmail);
    this.settingsForm2.controls['passOld'].setValue('');
    this.settingsForm2.controls['passNew'].setValue('');
  }

  resetPassword() {
    let userId = this.userId;

    this.userService.resetPassword('ResetPassword', { id: userId }).subscribe({
      next: () => {
        window.alert('done');
      },
      error: () => {
        window.alert('error');
      },
    });

    return false;
  }

  deleteUser() {
    let userId = this.userId;
    this.userService.deleteUser('DeleteUser', { id: userId }).subscribe({
      next: () => {
        window.alert('done');
      },
      error: () => {
        window.alert('error');
      },
    });
  }
}
