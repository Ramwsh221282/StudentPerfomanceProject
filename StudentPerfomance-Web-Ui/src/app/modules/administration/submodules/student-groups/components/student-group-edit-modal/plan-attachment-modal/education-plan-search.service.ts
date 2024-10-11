import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { Observable } from 'rxjs';
import { EducationPlan } from '../../../../education-plans/models/education-plan-interface';

@Injectable({
  providedIn: 'any',
})
export class EducationPlanSearchService {
  private readonly _httpClient: HttpClient;
  private readonly _apiUrii: string;

  public constructor() {
    this._httpClient = inject(HttpClient);
    this._apiUrii = `${BASE_API_URI}/education-plans/api/read/search`;
  }

  public getAll(): Observable<EducationPlan[]> {
    const apiUri: string = `${BASE_API_URI}/education-plans/api/read/all`;
    return this._httpClient.get<EducationPlan[]>(apiUri);
  }

  public search(plan: EducationPlan): Observable<EducationPlan[]> {
    const params = this.buildHttpParams(plan);
    return this._httpClient.get<EducationPlan[]>(this._apiUrii, { params });
  }

  private buildHttpParams(plan: EducationPlan): HttpParams {
    const params: HttpParams = new HttpParams()
      .set('Year', plan.year)
      .set('Direction.Code', plan.direction.code)
      .set('Direction.Name', plan.direction.name)
      .set('Direction.Type', plan.direction.type);
    return params;
  }
}
