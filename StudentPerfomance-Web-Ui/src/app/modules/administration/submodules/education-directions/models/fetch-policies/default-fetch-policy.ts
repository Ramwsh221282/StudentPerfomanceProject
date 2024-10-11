import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { EducationDirection } from '../education-direction-interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';

export class DefaultFetchPolicy implements IFetchPolicy<EducationDirection[]> {
  private readonly _baseApiUri = `${BASE_API_URI}/education-directions/api/read/byPage`;
  private _params: HttpParams;

  public constructor() {}

  public executeFetchPolicy(
    httpClient: HttpClient
  ): Observable<EducationDirection[]> {
    const params = this._params;
    return httpClient.get<EducationDirection[]>(this._baseApiUri, {
      params,
    });
  }

  public addPages(page: number, pageSize: number): void {
    this._params = new HttpParams().set('page', page).set('pageSize', pageSize);
  }
}
