import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Student } from '../../../../../students/models/student.interface';
//import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { StudentGroup } from '../../../../services/studentsGroup.interface';
import { AuthService } from '../../../../../../../users/services/auth.service';
import { AppConfigService } from '../../../../../../../../app.config.service';

export class NotActiveFetchPolicy implements IFetchPolicy<Student[]> {
  private readonly _apiUri: string;
  private _httpParams: HttpParams;
  private _httpHeaders: HttpHeaders;

  public constructor(
    group: StudentGroup,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    //this._apiUri = `${BASE_API_URI}/api/students/by-group-not-active-only`;
    this._apiUri = `${this._appConfig.baseApiUri}/api/students/by-group-not-active-only`;
    this.buildHttpHeaders();
    this.buildHttpParams(group);
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<Student[]> {
    const params = this._httpParams;
    const headers = this._httpHeaders;
    return httpClient.get<Student[]>(this._apiUri, {
      headers: headers,
      params,
    });
  }

  public addPages(page: number, pageSize: number): void {}

  private buildHttpHeaders(): void {
    this._httpHeaders = new HttpHeaders().set(
      'token',
      this._authService.userData.token,
    );
  }

  private buildHttpParams(group: StudentGroup): void {
    this._httpParams = new HttpParams().set('groupName', group.name);
  }
}
