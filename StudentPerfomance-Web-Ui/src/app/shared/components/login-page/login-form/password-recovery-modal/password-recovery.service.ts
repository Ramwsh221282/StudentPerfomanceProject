import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { BASE_API_URI } from '../../../../models/api/api-constants';
import { Observable } from 'rxjs';
import { AppConfigService } from '../../../../../app.config.service';

@Injectable({
  providedIn: 'root',
})
export class PasswordRecoveryService {
  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _appConfig: AppConfigService,
  ) {}

  public requestPasswordRecovery(email: string): Observable<any> {
    //const apiUri: string = `${BASE_API_URI}/app/users/password-recovery`;
    const apiUri: string = `${this._appConfig.baseApiUri}/app/users/password-recovery`;
    const payload = this.buildRequestBody(email);
    return this._httpClient.put<any>(apiUri, payload);
  }

  private buildRequestBody(email: string): object {
    return {
      command: {
        email: email,
      },
    };
  }
}
