import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { StudentGroup } from '../../services/studentsGroup.interface';
//import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../../../pages/user-page/services/auth.service';
import { AppConfigService } from '../../../../../../app.config.service';

export class DefaultFetchPolicy implements IFetchPolicy<StudentGroup[]> {
  private readonly _apiUri: string;
  private _httpHeaders: HttpHeaders;
  private _httpParams: HttpParams;

  public constructor(
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    this.buildHttpHeaders();
    //this._apiUri = `${BASE_API_URI}/api/student-groups`;
    this._apiUri = `${this._appConfig.baseApiUri}/api/student-groups`;
  }

  public executeFetchPolicy(
    httpClient: HttpClient,
  ): Observable<StudentGroup[]> {
    const headers = this._httpHeaders;
    const params = this._httpParams;
    return httpClient.get<StudentGroup[]>(this._apiUri, {
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
