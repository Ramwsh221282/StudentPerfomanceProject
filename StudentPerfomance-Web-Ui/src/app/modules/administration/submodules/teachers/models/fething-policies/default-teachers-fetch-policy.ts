import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Teacher } from '../teacher.interface';
import { Department } from '../../../departments/models/departments.interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { DepartmentPayloadBuilder } from '../../../departments/models/contracts/department-contract/department-payload-builder';
import { AuthService } from '../../../../../users/services/auth.service';
import { TokenPayloadBuilder } from '../../../../../../shared/models/common/token-contract/token-payload-builder';

export class DefaultTeacherFetchPolicy implements IFetchPolicy<Teacher[]> {
  private readonly _apiUri: string;
  private readonly _department: Department;
  private readonly _authService: AuthService;

  public constructor(department: Department, authService: AuthService) {
    this._apiUri = `${BASE_API_URI}/api/teachers/by-department`;
    this._department = department;
    this._authService = authService;
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<Teacher[]> {
    const payload = this.buildPayload();
    return httpClient.post<Teacher[]>(this._apiUri, payload);
  }

  public addPages(page: number, pageSize: number): void {}

  private buildPayload(): object {
    return {
      department: DepartmentPayloadBuilder(this._department),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
