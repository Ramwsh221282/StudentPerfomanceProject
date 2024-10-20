import { inject, Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';
import { EducationDirection } from '../models/education-direction-interface';
import { Observable } from 'rxjs';
import { User } from '../../../../users/services/user-interface';
import { AuthService } from '../../../../users/services/auth.service';

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
      `${this.managementApiUri}update`,
      body
    );
  }

  private buildPayload(
    initial: EducationDirection,
    updated: EducationDirection
  ): object {
    return {
      initial: initial,
      updated: updated,
      token: this._user.token,
    };
  }
}
