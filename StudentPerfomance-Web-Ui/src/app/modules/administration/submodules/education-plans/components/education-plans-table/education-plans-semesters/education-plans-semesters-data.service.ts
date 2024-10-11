import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { EducationPlan } from '../../../models/education-plan-interface';
import { Observable } from 'rxjs';
import { Semester } from '../../../../semesters/models/semester.interface';

@Injectable({
  providedIn: 'any',
})
export class EducationPlanSemestersService {
  private readonly _httpClient: HttpClient;
  private readonly _apiUri: string = `${BASE_API_URI}/semesters/api/read/education-plan-semesters`;

  public constructor() {
    this._httpClient = inject(HttpClient);
  }

  public getPlanSemesters(plan: EducationPlan): Observable<Semester[]> {
    const params = this.buildParameters(plan);
    return this._httpClient.get<Semester[]>(this._apiUri, { params });
  }

  private buildParameters(plan: EducationPlan): HttpParams {
    const params = new HttpParams()
      .set('schema.Year', plan.year)
      .set('schema.Direction.Code', plan.direction.code)
      .set('schema.Direction.Name', plan.direction.name)
      .set('schema.Direction.Type', plan.direction.type);
    return params;
  }
}
