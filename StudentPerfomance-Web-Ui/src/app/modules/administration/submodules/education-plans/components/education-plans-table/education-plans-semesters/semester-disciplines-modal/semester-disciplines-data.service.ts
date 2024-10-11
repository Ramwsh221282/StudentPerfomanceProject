import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { SemesterPlan } from '../../../../../semester-plans/models/semester-plan.interface';
import { Semester } from '../../../../../semesters/models/semester.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class SemesterDisciplinesDataService {
  private readonly _httpClient: HttpClient;
  private readonly _apiUri: string = `${BASE_API_URI}/semester-plans/api/read/by-semester`;

  public constructor() {
    this._httpClient = inject(HttpClient);
  }

  public getSemesterDisciplines(
    semester: Semester
  ): Observable<SemesterPlan[]> {
    const params = this.buildHttpParams(semester);
    return this._httpClient.get<SemesterPlan[]>(this._apiUri, { params });
  }

  private buildHttpParams(semester: Semester): HttpParams {
    const params: HttpParams = new HttpParams()
      .set('Semester.Number', semester.number)
      .set('Semester.Plan.Year', semester.plan.year)
      .set('Semester.Plan.Direction.Code', semester.plan.direction.code)
      .set('Semester.Plan.Direction.Name', semester.plan.direction.name)
      .set('Semester.Plan.Direction.Type', semester.plan.direction.type);
    return params;
  }
}
