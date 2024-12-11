import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { StudentGroup } from '../../services/studentsGroup.interface';
//import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../../users/services/auth.service';
import { AppConfigService } from '../../../../../../app.config.service';

export class FilterFetchPolicy implements IFetchPolicy<StudentGroup[]> {
  private readonly _apiUri: string;
  private readonly _group: StudentGroup;
  private _httpHeaders: HttpHeaders;
  private _httpParams: HttpParams;

  public constructor(
    group: StudentGroup,
    private readonly authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    this._group = group;
    this.buildHttpHeaders();
    //this._apiUri = `${BASE_API_URI}/api/student-groups/filter`;
    this._apiUri = `${this._appConfig.baseApiUri}/api/student-groups/filter`;
  }

  public executeFetchPolicy(
    httpClient: HttpClient,
  ): Observable<StudentGroup[]> {
    const params = this._httpParams;
    const headers = this._httpHeaders;
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
      .set('pageSize', pageSize)
      .set('groupName', this._group.name);
  }

  private buildHttpHeaders(): void {
    this._httpHeaders = new HttpHeaders().set(
      'token',
      this.authService.userData.token,
    );
  }
}
