import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../../../../users/services/auth.service';
import { DepartmentPayloadBuilder } from '../../../../models/contracts/department-contract/department-payload-builder';
import { TeacherPayloadBuilder } from '../../../../../teachers/contracts/teacher-contract/teacher-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../../../shared/models/common/token-contract/token-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class TeacherCreationService {
  private readonly _httpClient: HttpClient;
  private readonly _apiUri: string;

  public constructor(private readonly _authService: AuthService) {
    this._httpClient = inject(HttpClient);
    this._apiUri = `${BASE_API_URI}/api/teachers`;
  }

  public create(teacher: Teacher): Observable<Teacher> {
    const body = this.buildRequestBody(teacher);
    return this._httpClient.post<Teacher>(this._apiUri, body);
  }

  private buildRequestBody(teacher: Teacher): object {
    return {
      department: DepartmentPayloadBuilder(teacher.department),
      teacher: TeacherPayloadBuilder(teacher),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
