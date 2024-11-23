import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { IFetchPolicy } from '../../../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Student } from '../../../../../students/models/student.interface';
import { Observable } from 'rxjs';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { StudentGroup } from '../../../../services/studentsGroup.interface';
import { AuthService } from '../../../../../../../users/services/auth.service';

export class ActiveOnlyFetchPolicy implements IFetchPolicy<Student[]> {
  private readonly _apiUri: string;
  private _httpHeaders: HttpHeaders;
  private _httpParams: HttpParams;

  public constructor(
    group: StudentGroup,
    private readonly _authService: AuthService,
  ) {
    this._apiUri = `${BASE_API_URI}/api/students/active-only`;
    this.buildHttpParams(group);
    this.buildHttpHeaders();
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<Student[]> {
    const headers = this._httpHeaders;
    const params = this._httpParams;
    return httpClient.get<Student[]>(this._apiUri, {
      headers: headers,
      params,
    });
  }

  private buildHttpHeaders(): void {
    this._httpHeaders = new HttpHeaders().set(
      'token',
      this._authService.userData.token,
    );
  }

  private buildHttpParams(group: StudentGroup): void {
    this._httpParams = new HttpParams().set('groupName', group.name);
  }

  public addPages(page: number, pageSize: number): void {}
}
