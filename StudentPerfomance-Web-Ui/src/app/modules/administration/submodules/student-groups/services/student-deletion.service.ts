import { HttpClient, HttpHeaders } from '@angular/common/http';
//import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { inject, Injectable } from '@angular/core';
import { Student } from '../../students/models/student.interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../users/services/auth.service';
import { AppConfigService } from '../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class StudentDeletionService {
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

  public remove(student: Student): Observable<Student> {
    const headers = this.buildHttpHeaders();
    const body = this.buildRequestBody(student);
    return this._httpClient.delete<Student>(this._apiUri, {
      headers: headers,
      body,
    });
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
    };
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
