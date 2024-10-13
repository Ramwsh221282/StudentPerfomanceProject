import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { Student } from '../../../../students/models/student.interface';
import { Observable } from 'rxjs';

export class StudentCreationService {
  private readonly _httpClient: HttpClient;
  private readonly _apiUri: string;

  public constructor() {
    this._httpClient = inject(HttpClient);
    this._apiUri = `${BASE_API_URI}/student/api/management/create`;
  }

  public create(student: Student): Observable<Student> {
    const body = this.buildRequestBody(student);
    return this._httpClient.post<Student>(this._apiUri, body);
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
