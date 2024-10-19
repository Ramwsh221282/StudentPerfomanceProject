import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../shared/models/api/api-constants';
import { Observable } from 'rxjs';
import { User } from './user-interface';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly _httpClient: HttpClient;
  private readonly _cookieService: CookieService;
  private _user: User;
  private _isAuthorized = false;

  public constructor() {
    this._httpClient = inject(HttpClient);
    this._cookieService = inject(CookieService);
    this.tryAuthorizeUsingCookie();
  }

  public get userData(): User {
    return this._user;
  }

  public authorize(user: User): void {
    this._isAuthorized = true;
    this._cookieService.deleteAll();
    this._cookieService.set('token', user.token);
    this._cookieService.set('name', user.name);
    this._cookieService.set('surname', user.surname);
    this._cookieService.set('thirdname', user.thirdname);
    this._cookieService.set('email', user.email);
    this._cookieService.set('role', user.role);
    this._user = { ...user };
  }

  private tryAuthorizeUsingCookie(): void {
    const token = this._cookieService.get('token');
    if (token != '') {
      this._user = {
        name: this._cookieService.get('name'),
        surname: this._cookieService.get('surname'),
        thirdname: this._cookieService.get('thirdname'),
        email: this._cookieService.get('email'),
        token: this._cookieService.get('token'),
        role: this._cookieService.get('role'),
      } as User;
      this._isAuthorized = true;
      return;
    }
    this._isAuthorized = false;
  }

  public get isAuthorized(): boolean {
    return this._isAuthorized;
  }

  public login(payload: {
    user: { email: string; password: string };
  }): Observable<User> {
    const apiUri: string = `${BASE_API_URI}/api/auth/login`;
    return this._httpClient.post<User>(apiUri, payload);
  }
}
