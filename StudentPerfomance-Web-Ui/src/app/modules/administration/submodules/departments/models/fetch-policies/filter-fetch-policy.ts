import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Department } from '../departments.interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../../users/services/auth.service';

export class DepartmentFilterFetchPolicy implements IFetchPolicy<Department[]> {
  private readonly _department: Department;
  private readonly _authService: AuthService;
  private readonly _apiUri: string;
  private _httpHeaders: HttpHeaders;
  private _httpParams: HttpParams;

  public constructor(department: Department, authService: AuthService) {
    this._authService = authService;
    this._apiUri = `${BASE_API_URI}/api/teacher-departments/filter`;
    this._department = { ...department };
    this.buildHttpHeaders();
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<Department[]> {
    const headers = this._httpHeaders;
    const params = this._httpParams;
    return httpClient.get<Department[]>(this._apiUri, {
      headers: headers,
      params,
    });
  }

  public addPages(page: number, pageSize: number): void {
    this.buildHttpParams(page, pageSize);
  }

  private buildHttpParams(page: number, pageSize: number): void {
    this._httpParams = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize);
  }

  private buildHttpHeaders(): void {
    this._httpHeaders = new HttpHeaders().set(
      'token',
      this._authService.userData.token,
    );
  }
}
