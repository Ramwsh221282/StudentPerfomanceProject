import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationPlan } from '../models/education-plan-interface';
import { IFetchPolicy } from '../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { DefaultFetchPolicy } from '../models/fetch-policies/default-fetch-policy';
import { IObservableFetchable } from '../../../../../shared/models/fetch-policices/iobservable-fetchable.interface';
import { AuthService } from '../../../../users/services/auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class FetchService
  extends BaseService
  implements IObservableFetchable<EducationPlan[]>
{
  private _policy: IFetchPolicy<EducationPlan[]>;

  public constructor(private readonly _authService: AuthService) {
    super();
    this._policy = new DefaultFetchPolicy(this._authService);
  }

  public setPolicy(policy: IFetchPolicy<EducationPlan[]>): void {
    this._policy = policy;
  }

  public fetch(): Observable<EducationPlan[]> {
    return this._policy.executeFetchPolicy(this.httpClient);
  }

  public addPages(page: number, pageSize: number): void {
    this._policy.addPages(page, pageSize);
  }
}
