import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../../../../../pages/user-page/services/auth.service';
import { Observable } from 'rxjs';
import { TeacherAssignmentInfo } from '../../../models/teacher-assignment-info';
import { AppConfigService } from '../../../../../app.config.service';
import { AdminAccessResponse } from '../admin-assignments-access-resolver-dialog/admin-assignments-access.service';

@Injectable({
  providedIn: 'any',
})
export class TeacherAssignmentsDataService {
  private readonly _apiUri: string;

  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    //this._apiUri = `${BASE_API_URI}/app/assignment-sessions/teacher-assignments-info`;
    this._apiUri = `${this._appConfig.baseApiUri}/app/assignment-sessions/teacher-assignments-info`;
  }

  public getTeacherAssignmentsInfo(
    adminAccess: AdminAccessResponse | null = null,
  ): Observable<TeacherAssignmentInfo> {
    const headers =
      adminAccess == null
        ? this.buildHttpHeaders()
        : this.buildHttpHeadersWithAdminAccess(adminAccess);
    return this._httpClient.get<TeacherAssignmentInfo>(this._apiUri, {
      headers: headers,
    });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }

  private buildHttpHeadersWithAdminAccess(
    adminAccess: AdminAccessResponse,
  ): HttpHeaders {
    const access = `${adminAccess.adminId}#${adminAccess.teacherId}`;
    return new HttpHeaders()
      .set('token', this._authService.userData.token)
      .set('adminAssignmentsAccess', access);
  }
}
