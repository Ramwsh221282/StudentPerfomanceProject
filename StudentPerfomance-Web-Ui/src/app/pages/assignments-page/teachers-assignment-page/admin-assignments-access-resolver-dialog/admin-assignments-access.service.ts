import { Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../../../../shared/models/common/base-http/base-http.service';

@Injectable({
  providedIn: 'any',
})
export class AdminAssignmentsAccessService extends BaseHttpService {
  public tryGetAccess(teacherId: string): Observable<AdminAccessResponse> {
    const headers = this.buildHttpHeaders();
    const params = this.buildHttpParams(teacherId);
    const apiUrl = `${this._config.baseApiUri}/app/assignment-sessions/admin-assignments-access`;
    return this._http.get<AdminAccessResponse>(apiUrl, {
      headers,
      params,
    });
  }

  private buildHttpParams(teacherId: string): HttpParams {
    return new HttpParams().set('teacherId', teacherId);
  }
}

export interface AdminAccessResponse {
  adminId: string;
  teacherId: string;
}
