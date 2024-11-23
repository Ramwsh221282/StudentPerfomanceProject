import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { SemesterPlan } from '../../../../../semester-plans/models/semester-plan.interface';
import { Semester } from '../../../../../semesters/models/semester.interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../../../../users/services/auth.service';

@Injectable({
  providedIn: 'any',
})
export class SemesterDisciplinesDataService {
  private readonly _apiUri: string = `${BASE_API_URI}/api/semester-plans`;

  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService,
  ) {}

  public getSemesterDisciplines(
    semester: Semester,
  ): Observable<SemesterPlan[]> {
    const headers = this.buildHttpHeaders();
    const params = this.buildHttpParams(semester);
    return this._httpClient.get<SemesterPlan[]>(this._apiUri, {
      headers: headers,
      params,
    });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }

  private buildHttpParams(semester: Semester): HttpParams {
    return new HttpParams()
      .set('directionName', semester.educationPlan.direction.name)
      .set('directionCode', semester.educationPlan.direction.code)
      .set('directionType', semester.educationPlan.direction.type)
      .set('planYear', semester.educationPlan.year)
      .set('semesterNumber', semester.number);
  }
}
