import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
//import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { Observable } from 'rxjs';
import { EducationPlan } from '../../education-plans/models/education-plan-interface';
import { AuthService } from '../../../../../pages/user-page/services/auth.service';
import { EducationDirection } from '../../education-directions/models/education-direction-interface';
import { AppConfigService } from '../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class EducationPlanSearchService {
  private readonly _httpClient: HttpClient;

  public constructor(
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    this._httpClient = inject(HttpClient);
  }

  public search(plan: EducationPlan): Observable<EducationPlan[]> {
    //const apiUri: string = `${BASE_API_URI}/api/education-plans/search`;
    const apiUri: string = `${this._appConfig.baseApiUri}/api/education-plans/search`;
    const headers = this.buildHttpHeaders();
    const params = new HttpParams()
      .set('searchName', plan.direction.name)
      .set('searchCode', plan.direction.code)
      .set('searchType', plan.direction.type)
      .set('searchYear', plan.year);
    return this._httpClient.post<EducationPlan[]>(apiUri, {
      headers: headers,
      params,
    });
  }

  public getByDirection(
    direction: EducationDirection,
  ): Observable<EducationPlan[]> {
    //const apiUri: string = `${BASE_API_URI}/api/education-plans/by-education-direction`;
    const apiUri: string = `${this._appConfig.baseApiUri}/api/education-plans/by-education-direction`;
    const headers = this.buildHttpHeaders();
    const params = new HttpParams()
      .set('directionCode', direction.code)
      .set('directionName', direction.name)
      .set('directionType', direction.type);
    return this._httpClient.get<EducationPlan[]>(apiUri, {
      headers: headers,
      params,
    });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
