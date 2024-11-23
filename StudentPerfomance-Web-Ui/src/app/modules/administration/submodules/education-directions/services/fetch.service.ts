import { inject, Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationDirection } from '../models/education-direction-interface';
import { IFetchPolicy } from '../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { DefaultFetchPolicy } from '../models/fetch-policies/default-fetch-policy';
import { IObservableFetchable } from '../../../../../shared/models/fetch-policices/iobservable-fetchable.interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../users/services/auth.service';

@Injectable({
  providedIn: 'any',
})
export class FetchService
  extends BaseService
  implements IObservableFetchable<EducationDirection[]>
{
  private _currentPolicy: IFetchPolicy<EducationDirection[]>;
  private readonly _authService: AuthService;

  public constructor() {
    super();
    this._authService = inject(AuthService);
    this._currentPolicy = new DefaultFetchPolicy(this._authService);
  }

  public addPages(page: number, pageSize: number): void {
    this._currentPolicy.addPages(page, pageSize);
  }

  public fetch(): Observable<EducationDirection[]> {
    return this._currentPolicy.executeFetchPolicy(this.httpClient);
  }

  public setPolicy(policy: IFetchPolicy<EducationDirection[]>): void {
    this._currentPolicy = policy;
  }
}
