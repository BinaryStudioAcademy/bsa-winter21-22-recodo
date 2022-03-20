import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import jwt_decode from 'jwt-decode';
import { UserUpdateDto } from 'src/app/models/user/user-update-dto';
import { UserService } from 'src/app/services/user.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})
export class SettingsComponent implements OnInit {
  private readonly APIUrl = environment.apiUrl;
  public formAvatar: FormGroup = {} as FormGroup;
  public formPassword: FormGroup = {} as FormGroup;
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
    this.formAvatar = this.formBuilder.group({
      workspaceName: [
        this.name,
        {
          validators: [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(20),
            Validators.pattern("^[a-zA-Z`'][a-zA-Z-`' ]+[a-zA-Z`']?$"),
          ],
        },
      ],
    });

    this.formPassword = this.formBuilder.group({
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
            Validators.maxLength(20),
            Validators.pattern(
              /^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9*.!@#$%^&`(){}[\]:;<>,‘.?/~_+=|-]+)$/
            ),
          ],
        },
      ],
      passNew: [
        ,
        {
          validators: [
            Validators.required,
            Validators.minLength(8),
            Validators.maxLength(20),
            Validators.pattern(
              /^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9*.!@#$%^&`(){}[\]:;<>,‘.?/~_+=|-]+)$/
            ),
          ],
        },
      ],
    });
  }

  errorHandler(event: Event) {
    const target = event.target as HTMLInputElement;
    target.src = '../../assets/icons/test-user-logo.png';
  }

  saveNewInfo() {
    let workspaceName = this.formAvatar.controls['workspaceName'].value;

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
    this.formAvatar.controls['workspaceName'].setValue(this.oldName);
    this.avatar = '';
  }

  public handleFileInput(event: Event) {
    let target = event.target as HTMLInputElement;
    this.imageFile = target.files?.item(0) as File;

    if (!this.imageFile) {
      target.value = '';
      return;
    }

    if (this.imageFile.size / 1024 / 1024 > 5) {
      window.alert('Image can`t be heavier than ~5MB');
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
    let email = this.formPassword.controls['email'].value;
    let passCurrent = this.formPassword.controls['passOld'].value;
    let passNew = this.formPassword.controls['passNew'].value;
    let userId = this.userId;

    let userUpdateDto: UserUpdateDto = {
      id: userId,
      email,
      passwordCurrent: passCurrent,
      passwordNew: passNew,
    };

    this.userService
      .updatePassword('Update-Password-Email', userUpdateDto)
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
    this.formPassword.controls['email'].setValue(this.oldEmail);
    this.formPassword.controls['passOld'].setValue('');
    this.formPassword.controls['passNew'].setValue('');
  }

  resetPassword() {
    let userId = this.userId;

    this.userService.resetPassword(`Reset-Password/${userId}`).subscribe({
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
    this.userService.deleteUser(`Delete-User/${userId}`).subscribe({
      next: () => {
        window.alert('done');
      },
      error: () => {
        window.alert('error');
      },
    });
  }
}
