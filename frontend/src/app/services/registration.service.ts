import { HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { TokenDto } from "../models/token/token-dto";
import { AuthUserDto } from "../models/user/auth-user-dto";
import { UserRegisterDto } from "../models/user/user-register-dto";
import { ResourceService } from "./resource.service";

@Injectable({
    providedIn: 'root'
  })
export class RegistrationService extends ResourceService<UserRegisterDto> {

    getResourceUrl(): string {
        return '/Register'
    };

    public register(user: UserRegisterDto) {
        debugger
        return this._handleAuthResponse(this.add<UserRegisterDto,AuthUserDto>(user)).subscribe();
    }

    private _handleAuthResponse(observable: Observable<HttpResponse<AuthUserDto>>) {
        return observable.pipe(
            map((resp) => {
                this._setTokens(resp.body?.token as TokenDto);
                return resp.body?.user;
            })
        );
    }

    private _setTokens(tokens: TokenDto) {
        if (tokens && tokens.accessToken && tokens.refreshToken) {
            localStorage.setItem('accessToken', JSON.stringify(tokens.accessToken));
            localStorage.setItem('refreshToken', JSON.stringify(tokens.refreshToken));
        }
    }
}