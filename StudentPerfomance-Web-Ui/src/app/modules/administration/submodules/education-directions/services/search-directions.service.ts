import { inject, Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { IRequestParamsFactory } from '../../../../../models/RequestParamsFactory/irequest-params-factory.interface';
import { HttpParams } from '@angular/common/http';
import { EducationDirection } from '../models/education-direction-interface';
import { Observable } from 'rxjs';
import { User } from '../../../../users/services/user-interface';
import { AuthService } from '../../../../users/services/auth.service';

@Injectable({
  providedIn: 'any',
})
export class SearchDirectionsService extends BaseService {
  private readonly _user: User;

  public constructor() {
    super();
    const authService = inject(AuthService);
    this._user = { ...authService.userData };
  }

  public getAll(): Observable<EducationDirection[]> {
    const params: HttpParams = new HttpParams().set('token', this._user.token);
    return this.httpClient.get<EducationDirection[]>(`${this.readApiUri}all`, {
      params,
    });
  }

  public search(
    direction: EducationDirection
  ): Observable<EducationDirection[]> {
    const params = this.buildRequestParams(direction);
    return this.httpClient.get<EducationDirection[]>(
      `${this.readApiUri}search`,
      { params }
    );
  }

  private buildRequestParams(direction: EducationDirection): HttpParams {
    const params: HttpParams = new HttpParams()
      .set('Direction.Code', direction.code)
      .set('Direction.Name', direction.name)
      .set('Direction.Type', direction.type)
      .set('Token', this._user.token);
    return params;
  }
}
