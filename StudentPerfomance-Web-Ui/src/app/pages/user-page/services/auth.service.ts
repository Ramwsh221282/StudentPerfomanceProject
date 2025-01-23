import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError, Observable, tap } from 'rxjs';
import { User } from './user-interface';
import { CookieService } from 'ngx-cookie-service';
import { AppConfigService } from '../../../app.config.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly _httpClient: HttpClient;
  private readonly _cookieService: CookieService;
  private _user: User;
  private _isAuthorized = false;

  public constructor(private readonly _appConfig: AppConfigService) {
    this._httpClient = inject(HttpClient);
    this._cookieService = inject(CookieService);
    this.tryAuthorizeUsingCookie();
    //this.verify();
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
    this._cookieService.set('thirdname', user.patronymic);
    this._cookieService.set('email', user.email);
    this._cookieService.set('role', user.role);
    this._user = { ...user };
  }

  public tryAuthorizeUsingCookie(): void {
    const token = this._cookieService.get('token');
    if (token == '' || token == undefined) {
      this._user = {
        name: ' ',
        surname: ' ',
        patronymic: ' ',
        email: ' ',
        token: ' ',
        role: ' ',
      } as User;
      this._isAuthorized = false;
    }
    if (token != '') {
      this._user = {
        name: this._cookieService.get('name'),
        surname: this._cookieService.get('surname'),
        patronymic: this._cookieService.get('thirdname'),
        email: this._cookieService.get('email'),
        token: this._cookieService.get('token'),
        role: this._cookieService.get('role'),
      } as User;
      this._isAuthorized = true;
      return;
    }
  }

  public get isAuthorized(): boolean {
    return this._isAuthorized;
  }

  public login(payload: { email: string; password: string }): Observable<User> {
    const apiUri: string = `${this._appConfig.baseApiUri}/app/users/login`;
    //const apiUri: string = `${BASE_API_URI}/app/users/login`;
    return this._httpClient.post<User>(apiUri, payload);
  }

  public setNotAuthorized(): void {
    this._isAuthorized = false;
  }

  public verify(): void {
    const apiUri: string = `${this._appConfig.baseApiUri}/api/users/verify`;
    //const apiUri: string = `${BASE_API_URI}/app/users/verify`;
    const token = this._cookieService.get('token');
    this._httpClient
      .post(apiUri, { token: token })
      .pipe(
        tap(() => {
          this._isAuthorized = true;
        }),
        catchError((error: HttpErrorResponse) => {
          this._isAuthorized = false;
          return new Observable();
        }),
      )
      .subscribe();
  }
}
