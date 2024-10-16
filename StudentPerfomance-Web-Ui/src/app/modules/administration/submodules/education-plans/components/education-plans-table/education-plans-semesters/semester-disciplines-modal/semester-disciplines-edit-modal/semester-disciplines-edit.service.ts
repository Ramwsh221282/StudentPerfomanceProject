import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { SemesterPlan } from '../../../../../../semester-plans/models/semester-plan.interface';
import { Observable } from 'rxjs';
import { BASE_API_URI } from '../../../../../../../../../shared/models/api/api-constants';

@Injectable({
  providedIn: 'any',
})
export class SemesterDisciplinesEditService {
  private readonly _httpClient: HttpClient;

  public constructor() {
    this._httpClient = inject(HttpClient);
  }

  public attachTeacher(semesterPlan: SemesterPlan): Observable<SemesterPlan> {
    const apiUri: string = `${BASE_API_URI}/semester-plans/api/management/attach-teacher`;
    const body = {
      plan: { ...semesterPlan },
      teacher: { ...semesterPlan.teacher },
    };
    return this._httpClient.put<SemesterPlan>(apiUri, body);
  }

  public deattachTeacher(semesterPlan: SemesterPlan): Observable<SemesterPlan> {
    const apiUri: string = `${BASE_API_URI}/semester-plans/api/management/deattach-teacher`;
    const body = { plan: { ...semesterPlan } };
    return this._httpClient.put<SemesterPlan>(apiUri, body);
  }

  public changeName(
    initial: SemesterPlan,
    updated: SemesterPlan
  ): Observable<SemesterPlan> {
    const apiUri: string = `${BASE_API_URI}/semester-plans/api/management/update`;
    const body = {
      initial: { ...initial },
      updated: { ...updated },
    };
    return this._httpClient.put<SemesterPlan>(apiUri, body);
  }
}
