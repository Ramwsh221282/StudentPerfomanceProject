import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Teacher } from '../teacher.interface';
import { Department } from '../../../departments/models/departments.interface';
//import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../../../pages/user-page/services/auth.service';
import { AppConfigService } from '../../../../../../app.config.service';

export class DefaultTeacherFetchPolicy implements IFetchPolicy<Teacher[]> {
  private readonly _apiUri: string;
  private readonly _department: Department;
  private readonly _authService: AuthService;
  private _httpHeaders: HttpHeaders;
  private _httpParams: HttpParams;

  public constructor(
    department: Department,
    authService: AuthService,
    appConfig: AppConfigService,
  ) {
    //this._apiUri = `${BASE_API_URI}/api/teachers`;
    this._apiUri = `${appConfig.baseApiUri}/api/teachers`;
    this._department = department;
    this._authService = authService;
    this.buildHttpHeaders();
    this.buildHttpParams();
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<Teacher[]> {
    const headers = this._httpHeaders;
    const params = this._httpParams;
    return httpClient.get<Teacher[]>(this._apiUri, {
      headers: headers,
      params,
    });
  }

  public addPages(page: number, pageSize: number): void {}

  private buildHttpParams(): void {
    this._httpParams = new HttpParams().set(
      'departmentName',
      this._department.name,
    );
  }

  private buildHttpHeaders(): void {
    this._httpHeaders = new HttpHeaders().set(
      'token',
      this._authService.userData.token,
    );
  }
}
