import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppConfigService } from '../../../../../../../app.config.service';
import { AuthService } from '../../../../../../users/services/auth.service';
import { Student } from '../../../../students/models/student.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class SwitchStudentGroupService {
  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _appConfig: AppConfigService,
    private readonly _authService: AuthService,
  ) {}

  public switchStudentGroup(
    studentId: string,
    currentGroupId: string,
    newGroupId: string,
  ): Observable<Student> {
    const payload = this.buildPayload(studentId, currentGroupId, newGroupId);
    const headers = this.buildHttpHeaders();
    return this._httpClient.put<Student>(
      `${this._appConfig.baseApiUri}/api/students/change-group`,
      payload,
      { headers },
    );
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }

  private buildPayload(
    studentId: string,
    currentGroupId: string,
    newGroupId: string,
  ): object {
    return {
      studentId: studentId,
      currentGroupId: currentGroupId,
      otherGroupId: newGroupId,
    };
  }
}
