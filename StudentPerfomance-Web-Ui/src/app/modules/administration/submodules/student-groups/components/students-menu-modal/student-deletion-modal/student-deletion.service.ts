import { HttpClient } from '@angular/common/http';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { inject, Injectable } from '@angular/core';
import { Student } from '../../../../students/models/student.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class StudentDeletionService {
  private readonly _apiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor() {
    this._apiUri = `${BASE_API_URI}/student/api/management/remove`;
    this._httpClient = inject(HttpClient);
  }

  public remove(student: Student): Observable<Student> {
    const body = this.buildRequestBody(student);
    return this._httpClient.delete<Student>(this._apiUri, { body });
  }

  private buildRequestBody(student: Student): object {
    return {
      student: {
        name: student.name,
        surname: student.surname,
        thirdname: student.thirdname,
        state: student.state,
        recordbook: student.recordbook,
        group: {
          name: student.group.name,
          educationPlan: {
            year: student.group.plan.year,
            direction: {
              code: student.group.plan.direction.code,
              name: student.group.plan.direction.name,
              type: student.group.plan.direction.type,
            },
          },
        },
      },
    };
  }
}
