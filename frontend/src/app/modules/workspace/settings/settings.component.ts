import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import jwt_decode from 'jwt-decode';
import { of, switchMap } from 'rxjs';
import { GyazoUpload } from 'src/app/models/gyazo';
import { GyazoService } from 'src/app/services/gyazo.service';
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

  public oldName: any = '';
  public oldEmail: any = '';
  public userId: any;
  public name: any;
  public imageFile!: File;

  constructor(
    private gyazoService: GyazoService,
    private formBuilder: FormBuilder,
    private httpService: HttpClient
  ) {}

  ngOnInit(): void {
    this.initValues();
    this.initForms();
  }

  initValues() {
    let token = localStorage.getItem('accessToken') || '';

    if (token) {
      let decoded: any = jwt_decode(token);

      this.name = decoded.name;
      this.oldName = name;
      this.oldEmail = decoded.email;
      this.userId = decoded.id;
    }
  }

  initForms() {
    this.settingsForm1 = this.formBuilder.group({
      workspaceName: [
        this.name,
        {
          validators: [Validators.required, Validators.minLength(4)],
        },
      ],
    });

    this.settingsForm2 = this.formBuilder.group({
      email: [
        this.oldEmail,
        {
          validators: [Validators.required, Validators.minLength(6)],
        },
      ],
      passOld: [
        ,
        {
          validators: [Validators.required, Validators.minLength(8)],
        },
      ],
      passNew: [
        ,
        {
          validators: [Validators.minLength(8)],
        },
      ],
    });
  }

  errorHandler(event: any) {
    event.target.src = '../../assets/icons/test-user-logo.png';
  }

  saveNewInfoToDb(avatarLink: any) {
    let workspaceName = this.settingsForm1.controls['workspaceName'].value;
    let avatar = this.imageFile;

    const data = new FormData();
    data.append('id', this.userId);
    data.append('workspaceName', workspaceName);
    data.append('avatar', this.imageFile);

    this.httpService.post(`${this.APIUrl}/User/Update`, data).subscribe({
      next: (data) => {
        window.alert('done');
      },
      error: (data) => {
        window.alert('error');
      },
    });
  }

  saveNewInfo() {
    return this.saveNewInfoToDb(null);
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
      window.alert(`Image can't be heavier than ~5MB`);
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
        next: (data) => {
          window.alert('done');
        },
        error: (data) => {
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

    this.httpService
      .post(`${this.APIUrl}/User/ResetPassword`, {
        id: userId,
      })
      .subscribe({
        next: (data) => {
          window.alert('done');
        },
        error: (data) => {
          window.alert('error');
        },
      });

    return false;
  }

  deleteUser() {
    let userId = this.userId;

    this.httpService
      .post(`${this.APIUrl}/User/DeleteUser`, {
        userId,
      })
      .subscribe({
        next: (data) => {
          window.alert('done');
          localStorage.removeItem('accessToken');
        },
        error: (data) => {
          window.alert('error');
        },
      });
  }
}
