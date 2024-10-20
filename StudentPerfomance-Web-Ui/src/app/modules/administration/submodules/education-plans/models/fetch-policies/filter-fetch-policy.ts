import { HttpClient, HttpParams } from '@angular/common/http';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { EducationPlan } from '../education-plan-interface';
import { Observable } from 'rxjs';
import { User } from '../../../../../users/services/user-interface';
import { inject } from '@angular/core';
import { AuthService } from '../../../../../users/services/auth.service';

export class FilterFetchPolicy implements IFetchPolicy<EducationPlan[]> {
  private readonly _baseApiUri = `${BASE_API_URI}/education-plans/api/read/filter`;
  private readonly _plan: EducationPlan;
  private readonly _user: User;
  private _params: HttpParams;

  public constructor(plan: EducationPlan) {
    const authService = inject(AuthService);
    this._user = { ...authService.userData };
    this._plan = plan;
    this._params = new HttpParams()
      .set('Plan.Year', this._plan.year)
      .set('Plan.Direction.Code', this._plan.direction.code)
      .set('Plan.Direction.Name', this._plan.direction.name)
      .set('Plan.Direction.Type', this._plan.direction.type)
      .set('token', this._user.token);
  }

  public executeFetchPolicy(
    httpClient: HttpClient
  ): Observable<EducationPlan[]> {
    const params = this._params;
    return httpClient.get<EducationPlan[]>(this._baseApiUri, { params });
  }

  public addPages(page: number, pageSize: number): void {
    this._params = new HttpParams()
      .set('Plan.Year', this._plan.year)
      .set('Plan.Direction.Code', this._plan.direction.code)
      .set('Plan.Direction.Name', this._plan.direction.name)
      .set('Plan.Direction.Type', this._plan.direction.type)
      .set('Page', page)
      .set('PageSize', pageSize)
      .set('token', this._user.token);
  }
}
