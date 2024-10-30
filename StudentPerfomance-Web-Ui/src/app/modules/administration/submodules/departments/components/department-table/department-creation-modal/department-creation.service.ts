import { HttpClient } from '@angular/common/http';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { inject, Injectable } from '@angular/core';
import { Department } from '../../../models/departments.interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../../../users/services/auth.service';
import { DepartmentPayloadBuilder } from '../../../models/contracts/department-contract/department-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../../shared/models/common/token-contract/token-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class DepartmentCreationService {
  private readonly _apiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor(private readonly _authService: AuthService) {
    this._apiUri = `${BASE_API_URI}/api/teacher-departments`;
    this._httpClient = inject(HttpClient);
  }

  public create(department: Department): Observable<Department> {
    const body = this.buildRequestBody(department);
    return this._httpClient.post<Department>(this._apiUri, body);
  }

  private buildRequestBody(department: Department): object {
    return {
      department: DepartmentPayloadBuilder(department),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
