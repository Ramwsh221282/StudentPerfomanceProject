import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppConfigService } from '../../../../../app.config.service';

//import { BASE_API_URI } from '../../../../models/api/api-constants';

@Injectable({ providedIn: 'any' })
export class RecoveryConfirmationService {
  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _appConfig: AppConfigService,
  ) {}

  public confirmPasswordRecovery(token: string): Observable<any> {
    //const apiUri = `${BASE_API_URI}/reset-password`;
    const apiUri = `${this._appConfig.baseApiUri}/reset-password`;
    const params = this.buildHttpParams(token);
    return this._httpClient.get<any>(apiUri, { params });
  }

  private buildHttpParams(token: string): HttpParams {
    return new HttpParams().set('token', token);
  }
}
