import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../../../shared/models/common/base-http/base-http.service';
import { TeacherAssignmentInfo } from '../../assignments-page/models/teacher-assignment-info';
import { AdminAccessResponse } from '../../assignments-page/teachers-assignment-page/admin-assignments-access-resolver-dialog/admin-assignments-access.service';

@Injectable({
  providedIn: 'any',
})
export class TeacherAssignmentsDataService extends BaseHttpService {
  public getTeacherAssignmentsInfo(
    adminAccess: AdminAccessResponse | null = null,
  ): Observable<TeacherAssignmentInfo> {
    const url = `${this._config.baseApiUri}/app/assignment-sessions/teacher-assignments-info`;
    const headers =
      adminAccess == null
        ? this.buildHttpHeaders()
        : this.buildHttpHeadersWithAdminAccess(adminAccess);
    return this._http.get<TeacherAssignmentInfo>(url, {
      headers: headers,
    });
  }

  private buildHttpHeadersWithAdminAccess(
    adminAccess: AdminAccessResponse,
  ): HttpHeaders {
    const access = `${adminAccess.adminId}#${adminAccess.teacherId}`;
    return new HttpHeaders()
      .set('token', this._auth.userData.token)
      .set('adminAssignmentsAccess', access);
  }
}
