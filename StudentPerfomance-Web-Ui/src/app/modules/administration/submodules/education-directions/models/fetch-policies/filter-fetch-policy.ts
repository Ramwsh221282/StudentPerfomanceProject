import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { EducationDirection } from '../education-direction-interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { User } from '../../../../../users/services/user-interface';
import { AuthService } from '../../../../../users/services/auth.service';

export class FilterFetchPolicy implements IFetchPolicy<EducationDirection[]> {
  private readonly _baseApiUri = `${BASE_API_URI}/education-directions/api/read/filter`;
  private readonly _direction: EducationDirection;
  private readonly _user: User;
  private _httpParams: HttpParams;

  public constructor(
    direction: EducationDirection,
    private readonly authService: AuthService
  ) {
    this._direction = direction;
    this._httpParams = new HttpParams()
      .set('Code', direction.code)
      .set('Name', direction.name)
      .set('Type', direction.type);
    this._user = { ...authService.userData };
  }

  public executeFetchPolicy(
    httpClient: HttpClient
  ): Observable<EducationDirection[]> {
    const params = this._httpParams;
    return httpClient.get<EducationDirection[]>(this._baseApiUri, {
      params,
    });
  }

  public addPages(page: number, pageSize: number): void {
    this._httpParams = new HttpParams()
      .set('Direction.Code', this._direction.code)
      .set('Direction.Name', this._direction.name)
      .set('Direction.Type', this._direction.type)
      .set('page', page)
      .set('pageSize', pageSize)
      .set('token', this._user.token);
  }
}
