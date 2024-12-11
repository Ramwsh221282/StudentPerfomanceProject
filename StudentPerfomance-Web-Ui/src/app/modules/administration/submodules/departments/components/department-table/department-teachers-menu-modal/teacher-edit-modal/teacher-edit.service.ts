import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
//import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { Observable } from 'rxjs';
import { DepartmentPayloadBuilder } from '../../../../models/contracts/department-contract/department-payload-builder';
import { TeacherPayloadBuilder } from '../../../../../teachers/contracts/teacher-contract/teacher-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../../../shared/models/common/token-contract/token-payload-builder';
import { AuthService } from '../../../../../../../users/services/auth.service';
import { AppConfigService } from '../../../../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class TeacherEditService {
  private readonly _apiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor(
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    //this._apiUri = `${BASE_API_URI}/api/teachers`;
    this._apiUri = `${this._appConfig.baseApiUri}/api/teachers`;
    this._httpClient = inject(HttpClient);
    this.buildHttpHeaders();
  }

  public update(initial: Teacher, updated: Teacher): Observable<Teacher> {
    const header = this.buildHttpHeaders();
    const body = this.buildRequestBody(initial, updated);
    return this._httpClient.put<Teacher>(this._apiUri, body, {
      headers: header,
    });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }

  private buildRequestBody(initial: Teacher, updated: Teacher): object {
    return {
      department: DepartmentPayloadBuilder(initial.department),
      initial: TeacherPayloadBuilder(initial),
      updated: TeacherPayloadBuilder(updated),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
