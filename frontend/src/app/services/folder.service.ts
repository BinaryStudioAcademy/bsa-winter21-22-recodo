import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ResourceService } from './resource.service';
import { FolderDto } from '../models/folder/folder-dto';
import { NewFolderDto } from '../models/folder/new-folder-dto';

@Injectable({
  providedIn: 'root',
})
export class FolderService extends ResourceService<NewFolderDto> {
  private folder :FolderDto = {} as FolderDto;

  constructor(override httpClient: HttpClient) {
    super(httpClient);
  }

  getResourceUrl(): string {
    return '/folders'
  };

  public create(folder : NewFolderDto) {
    return this.handleAuthResponse(this.add<NewFolderDto,FolderDto>(folder));
  }

  private handleAuthResponse(observable: Observable<HttpResponse<FolderDto>>) {
    return observable.pipe(
        map((resp) => {
            this.folder = resp.body as FolderDto;
            return this.folder;
        })
    );
  }
}
