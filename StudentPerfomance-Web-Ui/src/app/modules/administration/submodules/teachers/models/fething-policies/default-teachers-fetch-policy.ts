import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Teacher } from '../teacher.interface';
import { Department } from '../../../departments/models/departments.interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';

export class DefaultTeacherFetchPolicy implements IFetchPolicy<Teacher[]> {
  private readonly _apiUri: string;
  private readonly _department: Department;
  private _httpParams: HttpParams;

  public constructor(department: Department) {
    this._apiUri = `${BASE_API_URI}/teacher/api/read/department-teachers`;
    this._department = department;
    this._httpParams = new HttpParams()
      .set('Department.Name', this._department.name)
      .set('Department.Shortname', department.shortName);
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<Teacher[]> {
    const params = this._httpParams;
    return httpClient.get<Teacher[]>(this._apiUri, { params });
  }

  public addPages(page: number, pageSize: number): void {}
}
