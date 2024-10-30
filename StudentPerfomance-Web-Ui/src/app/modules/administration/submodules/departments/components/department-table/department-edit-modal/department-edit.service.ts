import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { Department } from '../../../models/departments.interface';
import { Observable } from 'rxjs';
import { DepartmentPayloadBuilder } from '../../../models/contracts/department-contract/department-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../../shared/models/common/token-contract/token-payload-builder';
import { AuthService } from '../../../../../../users/services/auth.service';

@Injectable({
  providedIn: 'any',
})
export class DepartmentEditService {
  private readonly _apiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor(private readonly _authService: AuthService) {
    this._apiUri = `${BASE_API_URI}/api/teacher-departments`;
    this._httpClient = inject(HttpClient);
  }

  public update(
    initial: Department,
    updated: Department
  ): Observable<Department> {
    const body = this.buildRequestBody(initial, updated);
    return this._httpClient.put<Department>(this._apiUri, body);
  }

  private buildRequestBody(initial: Department, updated: Department): object {
    return {
      initial: DepartmentPayloadBuilder(initial),
      updated: DepartmentPayloadBuilder(updated),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
