import { HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { TokenDto } from '../models/token/token-dto';
import { AuthUserDto } from '../models/user/auth-user-dto';
import { UserDto } from '../models/user/user-dto';
import { UserRegisterDto } from '../models/user/user-register-dto';
import { ResourceService } from './resource.service';

@Injectable({
    providedIn: 'root'
  })
export class RegistrationService extends ResourceService<UserRegisterDto> {

    getResourceUrl(): string {
        return '/Register'
    };

    public register(user: UserRegisterDto) {
        debugger
        return this.handleAuthResponse(this.add<UserRegisterDto,AuthUserDto>(user));
    }

    public areTokensExist() {
        return localStorage.getItem('accessToken') && localStorage.getItem('refreshToken');
    }

    private handleAuthResponse(observable: Observable<HttpResponse<AuthUserDto>>) {
        return observable.pipe(
            map((resp) => {
                this.setTokens(resp.body?.token as TokenDto);
                return resp.body?.user as UserDto;
            })
        );
    }

    private setTokens(tokens: TokenDto) {
        if (tokens && tokens.accessToken && tokens.refreshToken) {
            localStorage.setItem('accessToken', JSON.stringify(tokens.accessToken));
            localStorage.setItem('refreshToken', JSON.stringify(tokens.refreshToken));
        }
    }
}