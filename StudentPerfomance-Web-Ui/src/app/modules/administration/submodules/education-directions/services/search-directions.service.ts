import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationDirection } from '../models/education-direction-interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../users/services/auth.service';
//import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { HttpHeaders, HttpParams } from '@angular/common/http';
import { AppConfigService } from '../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class SearchDirectionsService extends BaseService {
  public constructor(
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    super();
  }

  public getAll(): Observable<EducationDirection[]> {
    //const apiUri = `${BASE_API_URI}/api/education-direction/all`;
    const apiUri = `${this._appConfig.baseApiUri}/api/education-direction/all`;
    const headers = this.buildHttpHeaders();
    return this.httpClient.get<EducationDirection[]>(apiUri, {
      headers: headers,
    });
  }

  public search(
    direction: EducationDirection,
  ): Observable<EducationDirection[]> {
    //const apiUri = `${BASE_API_URI}/api/education-direction/search`;
    const apiUri = `${this._appConfig.baseApiUri}/api/education-direction/search`;
    const headers = this.buildHttpHeaders();
    const params = this.buildHttpParams(direction);
    return this.httpClient.get<EducationDirection[]>(apiUri, {
      headers: headers,
      params,
    });
  }

  private buildHttpParams(direction: EducationDirection): HttpParams {
    return new HttpParams()
      .set('searchCode', direction.code)
      .set('searchType', direction.type)
      .set('searchName', direction.name);
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
