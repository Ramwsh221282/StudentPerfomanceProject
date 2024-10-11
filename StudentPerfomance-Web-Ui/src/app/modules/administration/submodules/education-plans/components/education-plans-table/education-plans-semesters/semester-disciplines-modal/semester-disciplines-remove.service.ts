import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { SemesterPlan } from '../../../../../semester-plans/models/semester-plan.interface';
import { Observable } from 'rxjs';
import { Semester } from '../../../../../semesters/models/semester.interface';

@Injectable({
  providedIn: 'any',
})
export class SemesterDisciplinesRemoveService {
  private readonly _httpClient: HttpClient;
  private readonly _apiUri: string = `${BASE_API_URI}/semester-plans/api/management/remove`;

  public constructor() {
    this._httpClient = inject(HttpClient);
  }

  public remove(
    semester: Semester,
    plan: SemesterPlan
  ): Observable<SemesterPlan> {
    const body = this.buildRequestBody(semester, plan);
    return this._httpClient.delete<SemesterPlan>(this._apiUri, { body });
  }

  private buildRequestBody(semester: Semester, plan: SemesterPlan): object {
    return {
      semester: {
        number: semester.number,
        plan: {
          year: semester.plan.year,
          direction: {
            code: semester.plan.direction.code,
            name: semester.plan.direction.name,
            type: semester.plan.direction.type,
          },
        },
      },
      semesterPlan: {
        discipline: plan.discipline,
      },
    };
  }
}
