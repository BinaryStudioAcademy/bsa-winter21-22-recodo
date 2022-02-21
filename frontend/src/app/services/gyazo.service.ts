import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { GyazoUpload } from '../models/gyazo';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class GyazoService {
  constructor(private http: HttpClient) {}

  public uploadImage(image: Blob) {
    const data = new FormData();
    data.append('imagedata', image);

    return this.http.post<GyazoUpload>(
      this.buildUrl('https://upload.gyazo.com/api/upload'),
      data
    );
  }

  public deleteImage(imageId: string) {
    return this.http.delete(
      this.buildUrl(`https://api.gyazo.com/api/images/${imageId}`)
    );
  }

  private buildUrl(url: string) {
    return `${url}?access_token=${environment.gyazoAccessToken}`;
  }
}
