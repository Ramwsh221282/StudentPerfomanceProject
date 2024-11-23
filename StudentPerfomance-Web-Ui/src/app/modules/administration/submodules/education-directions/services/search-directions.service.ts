import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationDirection } from '../models/education-direction-interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../users/services/auth.service';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'any',
})
export class SearchDirectionsService extends BaseService {
  public constructor(private readonly _authService: AuthService) {
    super();
  }

  public getAll(): Observable<EducationDirection[]> {
    const headers = this.buildHttpHeaders();
    return this.httpClient.get<EducationDirection[]>(
      `${BASE_API_URI}/api/education-direction/all`,
      {
        headers: headers,
      },
    );
  }

  public search(
    direction: EducationDirection,
  ): Observable<EducationDirection[]> {
    const headers = this.buildHttpHeaders();
    const params = this.buildHttpParams(direction);
    return this.httpClient.get<EducationDirection[]>(
      `${BASE_API_URI}/api/education-direction/search`,
      {
        headers: headers,
        params,
      },
    );
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
