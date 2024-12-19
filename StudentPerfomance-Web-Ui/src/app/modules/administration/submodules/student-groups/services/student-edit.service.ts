import { HttpClient, HttpHeaders } from '@angular/common/http';
//import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { Student } from '../../students/models/student.interface';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StudentPayloadBuilder } from '../../students/models/contracts/student-contracts/student-payload-builder';
import { AuthService } from '../../../../users/services/auth.service';
import { AppConfigService } from '../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class StudentEditService {
  private readonly _apiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor(
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    //this._apiUri = `${BASE_API_URI}/api/students`;
    this._apiUri = `${this._appConfig.baseApiUri}/api/students`;
    this._httpClient = inject(HttpClient);
  }

  public update(initial: Student, updated: Student): Observable<Student> {
    const body = this.buildRequestBody(initial, updated);
    const headers = this.buildHttpHeaders();
    return this._httpClient.put<Student>(this._apiUri, body, {
      headers: headers,
    });
  }

  private buildRequestBody(initial: Student, updated: Student): object {
    return {
      student: StudentPayloadBuilder(initial),
      updated: StudentPayloadBuilder(updated),
    };
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
