import { inject, Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationDirection } from '../models/education-direction-interface';
import { Observable } from 'rxjs';
import { User } from '../../../../users/services/user-interface';
import { AuthService } from '../../../../users/services/auth.service';

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
      `${this.managementApiUri}remove`,
      {
        body,
      }
    );
  }

  private buildPayload(direction: EducationDirection): object {
    return {
      direction: direction,
      token: this._user.token,
    };
  }
}
