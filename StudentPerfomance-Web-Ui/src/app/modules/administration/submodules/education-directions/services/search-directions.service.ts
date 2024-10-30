import { inject, Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationDirection } from '../models/education-direction-interface';
import { Observable } from 'rxjs';
import { User } from '../../../../users/services/user-interface';
import { AuthService } from '../../../../users/services/auth.service';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { DirectionPayloadBuilder } from '../models/contracts/direction-payload-builder';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class SearchDirectionsService extends BaseService {
  private readonly _user: User;

  public constructor() {
    super();
    const authService = inject(AuthService);
    this._user = { ...authService.userData };
  }

  public getAll(): Observable<EducationDirection[]> {
    return this.httpClient.post<EducationDirection[]>(
      `${BASE_API_URI}/api/education-direction/all`,
      {
        token: TokenPayloadBuilder(this._user),
      }
    );
  }

  public search(
    direction: EducationDirection
  ): Observable<EducationDirection[]> {
    const payload = this.buildPayload(direction);
    return this.httpClient.post<EducationDirection[]>(
      `${BASE_API_URI}/api/education-direction/search`,
      payload
    );
  }

  private buildPayload(direction: EducationDirection): object {
    return {
      direction: DirectionPayloadBuilder(direction),
      token: TokenPayloadBuilder(this._user),
    };
  }
}
