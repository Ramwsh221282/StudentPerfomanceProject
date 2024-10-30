import { inject, Injectable } from '@angular/core';
import { EducationDirection } from '../models/education-direction-interface';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { User } from '../../../../users/services/user-interface';
import { AuthService } from '../../../../users/services/auth.service';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';
import { DirectionPayloadBuilder } from '../models/contracts/direction-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class CreateService extends BaseService {
  private readonly _user: User;

  constructor() {
    super();
    const authService = inject(AuthService);
    this._user = { ...authService.userData };
  }

  public create(direction: EducationDirection): Observable<EducationDirection> {
    const body = this.buildPayload(direction);
    return this.httpClient.post<EducationDirection>(
      `${BASE_API_URI}/api/education-direction`,
      body
    );
  }

  private buildPayload(direction: EducationDirection): object {
    return {
      direction: DirectionPayloadBuilder(direction),
      token: TokenPayloadBuilder(this._user),
    };
  }
}
