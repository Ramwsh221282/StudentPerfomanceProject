import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { Observable } from 'rxjs';
import { DepartmentPayloadBuilder } from '../../../../models/contracts/department-contract/department-payload-builder';
import { TeacherPayloadBuilder } from '../../../../../teachers/contracts/teacher-contract/teacher-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../../../shared/models/common/token-contract/token-payload-builder';
import { AuthService } from '../../../../../../../users/services/auth.service';

@Injectable({
  providedIn: 'any',
})
export class TeacherRemoveService {
  private readonly _apiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor(private readonly _authService: AuthService) {
    this._httpClient = inject(HttpClient);
    this._apiUri = `${BASE_API_URI}/api/teachers`;
  }

  public remove(teacher: Teacher): Observable<Teacher> {
    const body = this.buildRequestBody(teacher);
    return this._httpClient.delete<Teacher>(this._apiUri, { body });
  }

  private buildRequestBody(teacher: Teacher): object {
    return {
      department: DepartmentPayloadBuilder(teacher.department),
      teacher: TeacherPayloadBuilder(teacher),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
