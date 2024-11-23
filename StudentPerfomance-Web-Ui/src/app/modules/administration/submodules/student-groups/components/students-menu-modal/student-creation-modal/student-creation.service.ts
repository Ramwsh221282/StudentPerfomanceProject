import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { Student } from '../../../../students/models/student.interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../../../users/services/auth.service';
import { StudentPayloadBuilder } from '../../../../students/models/contracts/student-contracts/student-payload-builder';

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
    const headers = this.buildHttpHeaders();
    return this._httpClient.post<Student>(this._apiUri, body, {
      headers: headers,
    });
  }

  private buildRequestBody(student: Student): object {
    return {
      student: StudentPayloadBuilder(student),
    };
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
