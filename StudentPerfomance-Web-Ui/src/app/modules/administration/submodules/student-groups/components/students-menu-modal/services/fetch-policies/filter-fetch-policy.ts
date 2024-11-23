import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Student } from '../../../../../students/models/student.interface';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../../../../users/services/auth.service';

export class FilterFetchPolicy implements IFetchPolicy<Student[]> {
  private readonly _apiUri: string;
  private _httpHeaders: HttpHeaders;
  private _httpParams: HttpParams;

  public constructor(
    student: Student,
    private readonly _authService: AuthService,
  ) {
    this._apiUri = `${BASE_API_URI}/api/students/filter`;
    this.buildHttpHeaders();
    this.buildHttpParams(student);
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

  private buildHttpParams(student: Student): void {
    this._httpParams = new HttpParams()
      .set('groupName', student.group.name)
      .set('name', student.name)
      .set('surname', student.surname)
      .set('patronymic', student.patronymic)
      .set('state', student.state)
      .set('recordBook', student.recordbook);
  }
}
