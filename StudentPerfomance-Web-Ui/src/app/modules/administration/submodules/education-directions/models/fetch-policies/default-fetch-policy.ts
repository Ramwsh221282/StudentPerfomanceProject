import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { EducationDirection } from '../education-direction-interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { User } from '../../../../../users/services/user-interface';
import { AuthService } from '../../../../../users/services/auth.service';

export class DefaultFetchPolicy implements IFetchPolicy<EducationDirection[]> {
  private readonly _baseApiUri = `${BASE_API_URI}/education-directions/api/read/byPage`;
  private _params: HttpParams;
  private _user: User;

  public constructor(private readonly authService: AuthService) {
    this._user = { ...authService.userData };
  }

  public executeFetchPolicy(
    httpClient: HttpClient
  ): Observable<EducationDirection[]> {
    const params = this._params;
    return httpClient.get<EducationDirection[]>(this._baseApiUri, {
      params,
    });
  }

  public addPages(page: number, pageSize: number): void {
    this._params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize)
      .set('Token', this._user.token);
  }
}
