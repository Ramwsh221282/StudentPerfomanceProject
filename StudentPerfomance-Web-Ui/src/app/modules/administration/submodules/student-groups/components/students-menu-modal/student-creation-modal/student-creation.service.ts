import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { Student } from '../../../../students/models/student.interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../../../users/services/auth.service';
import { StudentPayloadBuilder } from '../../../../students/models/contracts/student-contracts/student-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../../shared/models/common/token-contract/token-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class StudentCreationService {
  private readonly _httpClient: HttpClient;
  private readonly _apiUri: string;

  public constructor(private readonly _authService: AuthService) {
    this._httpClient = inject(HttpClient);
    this._apiUri = `${BASE_API_URI}/api/students`;
  }

  public create(student: Student): Observable<Student> {
    const body = this.buildRequestBody(student);
    return this._httpClient.post<Student>(this._apiUri, body);
  }

  private buildRequestBody(student: Student): object {
    return {
      student: StudentPayloadBuilder(student),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
