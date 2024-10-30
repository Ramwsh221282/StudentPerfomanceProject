import { HttpClient } from '@angular/common/http';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { Student } from '../../../../students/models/student.interface';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StudentPayloadBuilder } from '../../../../students/models/contracts/student-contracts/student-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../../shared/models/common/token-contract/token-payload-builder';
import { AuthService } from '../../../../../../users/services/auth.service';

@Injectable({
  providedIn: 'any',
})
export class StudentEditService {
  private readonly _apiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor(private readonly _authService: AuthService) {
    this._apiUri = `${BASE_API_URI}/api/students`;
    this._httpClient = inject(HttpClient);
  }

  public update(initial: Student, updated: Student): Observable<Student> {
    const body = this.buildRequestBody(initial, updated);
    return this._httpClient.put<Student>(this._apiUri, body);
  }

  private buildRequestBody(initial: Student, updated: Student): object {
    return {
      student: StudentPayloadBuilder(initial),
      updated: StudentPayloadBuilder(updated),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
