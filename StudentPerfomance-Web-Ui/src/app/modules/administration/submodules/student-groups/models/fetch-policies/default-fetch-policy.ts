import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';

export class DefaultFetchPolicy implements IFetchPolicy<StudentGroup[]> {
  private readonly _apiUri: string = `${BASE_API_URI}/student-groups/api/read/byPage`;
  private _params: HttpParams;

  public constructor() {
    this._params = new HttpParams();
  }

  public executeFetchPolicy(
    httpClient: HttpClient
  ): Observable<StudentGroup[]> {
    const params = this._params;
    return httpClient.get<StudentGroup[]>(this._apiUri, { params });
  }

  addPages(page: number, pageSize: number): void {
    this._params = new HttpParams().set('page', page).set('pageSize', pageSize);
  }
}
