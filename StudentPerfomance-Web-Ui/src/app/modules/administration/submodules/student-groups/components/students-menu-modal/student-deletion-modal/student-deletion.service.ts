import { HttpClient } from '@angular/common/http';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { inject, Injectable } from '@angular/core';
import { Student } from '../../../../students/models/student.interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../../../users/services/auth.service';
import { StudentPayloadBuilder } from '../../../../students/models/contracts/student-contracts/student-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../../shared/models/common/token-contract/token-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class StudentDeletionService {
  private readonly _apiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor(private readonly _authService: AuthService) {
    this._apiUri = `${BASE_API_URI}/api/students`;
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
        patronymic: student.patronymic,
        state: student.state,
        recordbook: student.recordbook,
        groupName: student.group.name,
      },
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
