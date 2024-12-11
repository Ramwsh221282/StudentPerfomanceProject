import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Student } from '../../../../../students/models/student.interface';
import { StudentGroup } from '../../../../services/studentsGroup.interface';
//import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../../../../users/services/auth.service';
import { AppConfigService } from '../../../../../../../../app.config.service';

export class DefaultFetchPolicy implements IFetchPolicy<Student[]> {
  private readonly _apiUri: string;
  private _httpHeaders: HttpHeaders;
  private _httpParams: HttpParams;

  public constructor(
    group: StudentGroup,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    //this._apiUri = `${BASE_API_URI}/api/students`;
    this._apiUri = `${this._appConfig.baseApiUri}/api/students`;
    this.buildHttpHeaders();
    this.buildHttpParams(group);
  }

  executeFetchPolicy(httpClient: HttpClient): Observable<Student[]> {
    const headers = this._httpHeaders;
    const params = this._httpParams;
    return httpClient.get<Student[]>(this._apiUri, {
      headers: headers,
      params,
    });
  }

  addPages(page: number, pageSize: number): void {}

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
