import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Department } from '../departments.interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../../users/services/auth.service';

export class DepartmentDefaultFetchPolicy
  implements IFetchPolicy<Department[]>
{
  private readonly _apiUri: string;
  private readonly _authService: AuthService;
  private _headers: HttpHeaders;
  private _params: HttpParams;

  public constructor(authService: AuthService) {
    this._apiUri = `${BASE_API_URI}/api/teacher-departments`;
    this._authService = authService;
    this.buildHttpHeaders();
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<Department[]> {
    const headers = this._headers;
    const params = this._params;
    return httpClient.get<Department[]>(this._apiUri, {
      headers: headers,
      params,
    });
  }

  public addPages(page: number, pageSize: number): void {
    this.buildHttpParams(page, pageSize);
  }

  private buildHttpParams(page: number, pageSize: number): void {
    this._params = new HttpParams().set('page', page).set('pageSize', pageSize);
  }

  private buildHttpHeaders(): void {
    this._headers = new HttpHeaders().set(
      'token',
      this._authService.userData.token,
    );
  }
}
