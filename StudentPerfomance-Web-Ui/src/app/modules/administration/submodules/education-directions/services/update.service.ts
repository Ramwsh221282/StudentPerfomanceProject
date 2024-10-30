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
export class UpdateService extends BaseService {
  private readonly _user: User;

  public constructor() {
    super();
    const authService = inject(AuthService);
    this._user = { ...authService.userData };
  }

  public update(
    initial: EducationDirection,
    updated: EducationDirection
  ): Observable<EducationDirection> {
    const body = this.buildPayload(initial, updated);
    return this.httpClient.put<EducationDirection>(
      `${BASE_API_URI}/api/education-direction`,
      body
    );
  }

  private buildPayload(
    initial: EducationDirection,
    updated: EducationDirection
  ): object {
    return {
      initial: DirectionPayloadBuilder(initial),
      updated: DirectionPayloadBuilder(updated),
      token: TokenPayloadBuilder(this._user),
    };
  }
}
