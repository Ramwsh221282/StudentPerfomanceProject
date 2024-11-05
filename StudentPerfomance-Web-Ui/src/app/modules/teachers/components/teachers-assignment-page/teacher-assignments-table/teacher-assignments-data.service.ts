import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../../../../users/services/auth.service';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { Observable } from 'rxjs';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';
import { TeacherAssignmentInfo } from '../../../models/teacher-assignment-info';

@Injectable({
  providedIn: 'any',
})
export class TeacherAssignmentsDataService {
  private readonly _apiUri: string;
  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService
  ) {
    this._apiUri = `${BASE_API_URI}/app/assignment-sessions/teacher-assignments-info`;
  }

  public getTeacherAssignmentsInfo(): Observable<TeacherAssignmentInfo> {
    const payload = {
      token: TokenPayloadBuilder(this._authService.userData),
    };
    return this._httpClient.post<TeacherAssignmentInfo>(this._apiUri, payload);
  }
}
