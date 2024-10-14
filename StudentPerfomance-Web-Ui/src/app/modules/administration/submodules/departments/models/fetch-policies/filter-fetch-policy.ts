import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Department } from '../departments.interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';

export class DepartmentFilterFetchPolicy implements IFetchPolicy<Department[]> {
  private readonly _department: Department;
  private readonly _apiUri: string;
  private _httpParams: HttpParams;

  public constructor(department: Department) {
    this._apiUri = `${BASE_API_URI}/teacher-departments/api/read/filter`;
    this._department = { ...department };
    this._httpParams = new HttpParams()
      .set('Department.Name', this._department.name)
      .set('Department.Shortname', this._department.shortName);
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<Department[]> {
    const params = this._httpParams;
    return httpClient.get<Department[]>(this._apiUri, { params });
  }

  public addPages(page: number, pageSize: number): void {
    this._httpParams = new HttpParams()
      .set('Department.Name', this._department.name)
      .set('Department.Shortname', this._department.shortName)
      .set('Page', page)
      .set('PageSize', pageSize);
  }
}
