import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
//import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../../../../users/services/auth.service';
import { DepartmentPayloadBuilder } from '../../../../models/contracts/department-contract/department-payload-builder';
import { TeacherPayloadBuilder } from '../../../../../teachers/contracts/teacher-contract/teacher-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../../../shared/models/common/token-contract/token-payload-builder';
import { AppConfigService } from '../../../../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class TeacherCreationService {
  private readonly _httpClient: HttpClient;
  private readonly _apiUri: string;
  private _httpHeaders: HttpHeaders;

  public constructor(
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    this._httpClient = inject(HttpClient);
    //this._apiUri = `${BASE_API_URI}/api/teachers`;
    this._apiUri = `${this._appConfig.baseApiUri}/api/teachers`;
    this.buildHttpHeaders();
  }

  public create(teacher: Teacher): Observable<Teacher> {
    const headers = this._httpHeaders;
    const body = this.buildRequestBody(teacher);
    return this._httpClient.post<Teacher>(this._apiUri, body, {
      headers: headers,
    });
  }

  private buildHttpHeaders(): void {
    this._httpHeaders = new HttpHeaders().set(
      'token',
      this._authService.userData.token,
    );
  }

  private buildRequestBody(teacher: Teacher): object {
    return {
      department: DepartmentPayloadBuilder(teacher.department),
      teacher: TeacherPayloadBuilder(teacher),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
