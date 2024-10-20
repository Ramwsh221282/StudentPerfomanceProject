import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { EducationPlan } from '../education-plan-interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { User } from '../../../../../users/services/user-interface';
import { inject } from '@angular/core';
import { AuthService } from '../../../../../users/services/auth.service';

export class DefaultFetchPolicy implements IFetchPolicy<EducationPlan[]> {
  private readonly _baseApiUri = `${BASE_API_URI}/education-plans/api/read/byPage`;
  private readonly _user: User;
  private _params: HttpParams;

  public constructor() {
    this._params = new HttpParams().set('page', 1).set('pageSize', 10);
    const authService = inject(AuthService);
    this._user = { ...authService.userData };
  }

  public executeFetchPolicy(
    httpClient: HttpClient
  ): Observable<EducationPlan[]> {
    const params = this._params;
    return httpClient.get<EducationPlan[]>(this._baseApiUri, { params });
  }

  public addPages(page: number, pageSize: number): void {
    this._params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize)
      .set('Token', this._user.token);
  }
}
