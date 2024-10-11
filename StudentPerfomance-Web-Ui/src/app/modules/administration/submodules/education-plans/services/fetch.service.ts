import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationPlan } from '../models/education-plan-interface';
import { IFetchable } from '../../../../../shared/models/fetch-policices/ifetchable-interface';
import { IFetchPolicy } from '../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { DefaultFetchPolicy } from '../models/fetch-policies/default-fetch-policy';

@Injectable({
  providedIn: 'any',
})
export class FetchService
  extends BaseService
  implements IFetchable<EducationPlan[]>
{
  private _policy: IFetchPolicy<EducationPlan[]>;
  private _plans: EducationPlan[] = [];
  public constructor() {
    super();
    this._policy = new DefaultFetchPolicy();
  }

  public setPolicy(policy: IFetchPolicy<EducationPlan[]>): void {
    this._policy = policy;
  }

  public fetch(): void {
    this._policy.executeFetchPolicy(this.httpClient).subscribe((response) => {
      this._plans = response;
    });
  }

  public addPages(page: number, pageSize: number): void {
    this._policy.addPages(page, pageSize);
  }

  public get Plans(): EducationPlan[] {
    return this._plans;
  }
}
