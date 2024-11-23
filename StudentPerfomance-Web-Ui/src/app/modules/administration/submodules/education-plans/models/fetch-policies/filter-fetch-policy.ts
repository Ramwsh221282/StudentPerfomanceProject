import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { EducationPlan } from '../education-plan-interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../../users/services/auth.service';

export class FilterFetchPolicy implements IFetchPolicy<EducationPlan[]> {
  private readonly _baseApiUri = `${BASE_API_URI}/api/education-plans/filter`;
  private readonly _authService: AuthService;
  private readonly _plan: EducationPlan;
  private _headers: HttpHeaders;
  private _params: HttpParams;

  public constructor(plan: EducationPlan, authService: AuthService) {
    this._authService = authService;
    this._plan = plan;
    this.buildHeaders();
  }

  public executeFetchPolicy(
    httpClient: HttpClient,
  ): Observable<EducationPlan[]> {
    const headers = this._headers;
    const params = this._params;
    return httpClient.get<EducationPlan[]>(this._baseApiUri, {
      headers: headers,
      params,
    });
  }

  public addPages(page: number, pageSize: number): void {
    this.buildParams(page, pageSize);
  }

  private buildHeaders(): void {
    this._headers = new HttpHeaders().set(
      'token',
      this._authService.userData.token,
    );
  }

  private buildParams(page: number, pageSize: number): void {
    this._params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize)
      .set('filterCode', this._plan.direction.code)
      .set('filterName', this._plan.direction.name)
      .set('filterType', this._plan.direction.type)
      .set('filterYear', this._plan.year);
  }
}
