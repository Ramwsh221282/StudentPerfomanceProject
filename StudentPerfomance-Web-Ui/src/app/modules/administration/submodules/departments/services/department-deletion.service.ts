import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
//import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { Department } from '../models/departments.interface';
import { Observable } from 'rxjs';
import { DepartmentPayloadBuilder } from '../models/contracts/department-contract/department-payload-builder';
import { AuthService } from '../../../../../pages/user-page/services/auth.service';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';
import { AppConfigService } from '../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class DepartmentDeletionService {
  private readonly _apiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor(
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    //this._apiUri = `${BASE_API_URI}/api/teacher-departments`;
    this._apiUri = `${this._appConfig.baseApiUri}/api/teacher-departments`;
    this._httpClient = inject(HttpClient);
  }

  public remove(department: Department): Observable<Department> {
    const headers = this.buildHttpHeaders();
    const body = this.buildRequestBody(department);
    return this._httpClient.delete<Department>(this._apiUri, {
      headers: headers,
      body,
    });
  }

  private buildRequestBody(department: Department): object {
    return {
      department: DepartmentPayloadBuilder(department),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
