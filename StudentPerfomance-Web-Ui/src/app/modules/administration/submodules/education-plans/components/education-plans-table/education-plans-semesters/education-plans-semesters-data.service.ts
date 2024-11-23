import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { EducationPlan } from '../../../models/education-plan-interface';
import { Observable } from 'rxjs';
import { Semester } from '../../../../semesters/models/semester.interface';
import { AuthService } from '../../../../../../users/services/auth.service';

@Injectable({
  providedIn: 'any',
})
export class EducationPlanSemestersService {
  private readonly _apiUri: string = `${BASE_API_URI}/api/semesters`;

  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService,
  ) {}

  public getPlanSemesters(plan: EducationPlan): Observable<Semester[]> {
    const headers = this.buildHttpHeaders();
    const params = this.buildHttpParams(plan);
    return this._httpClient.get<Semester[]>(this._apiUri, {
      headers: headers,
      params,
    });
  }

  private buildHttpParams(plan: EducationPlan): HttpParams {
    return new HttpParams()
      .set('directionName', plan.direction.name)
      .set('directionCode', plan.direction.code)
      .set('directionType', plan.direction.type)
      .set('planYear', plan.year);
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
