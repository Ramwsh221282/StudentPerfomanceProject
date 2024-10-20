import { inject, Injectable } from '@angular/core';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';
import { EducationDirection } from '../models/education-direction-interface';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { User } from '../../../../users/services/user-interface';
import { AuthService } from '../../../../users/services/auth.service';

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
      `${this.managementApiUri}create`,
      body
    );
  }

  private buildPayload(direction: EducationDirection): object {
    return {
      direction: direction,
      token: this._user.token,
    };
  }
}
