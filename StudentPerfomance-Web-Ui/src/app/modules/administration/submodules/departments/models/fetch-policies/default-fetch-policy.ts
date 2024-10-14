import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Department } from '../departments.interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';

export class DepartmentDefaultFetchPolicy
  implements IFetchPolicy<Department[]>
{
  private readonly _apiUri: string;
  private _httpParams: HttpParams;

  public constructor() {
    this._apiUri = `${BASE_API_URI}/teacher-departments/api/read/byPage`;
    this._httpParams = new HttpParams();
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<Department[]> {
    const params = this._httpParams;
    return httpClient.get<Department[]>(this._apiUri, { params });
  }

  public addPages(page: number, pageSize: number): void {
    this._httpParams = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize);
  }
}
