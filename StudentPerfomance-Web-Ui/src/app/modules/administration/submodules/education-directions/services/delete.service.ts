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
export class DeleteService extends BaseService {
  private readonly _user: User;

  constructor() {
    super();
    const authService = inject(AuthService);
    this._user = { ...authService.userData };
  }

  public delete(direction: EducationDirection): Observable<EducationDirection> {
    const body = this.buildPayload(direction);
    return this.httpClient.delete<EducationDirection>(
      `${BASE_API_URI}/api/education-direction`,
      {
        body,
      }
    );
  }

  private buildPayload(direction: EducationDirection): object {
    return {
      direction: DirectionPayloadBuilder(direction),
      token: TokenPayloadBuilder(this._user),
    };
  }
}
