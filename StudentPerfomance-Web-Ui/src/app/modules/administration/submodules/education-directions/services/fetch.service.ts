import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationDirection } from '../models/education-direction-interface';
import { IFetchable } from '../../../../../shared/models/fetch-policices/ifetchable-interface';
import { IFetchPolicy } from '../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { DefaultFetchPolicy } from '../models/fetch-policies/default-fetch-policy';

@Injectable({
  providedIn: 'any',
})
export class FetchService
  extends BaseService
  implements IFetchable<EducationDirection[]>
{
  private _currentPolicy: IFetchPolicy<EducationDirection[]>;
  private _directions: EducationDirection[];

  public constructor() {
    super();
    this._directions = [];
    this._currentPolicy = new DefaultFetchPolicy();
  }

  public addPages(page: number, pageSize: number): void {
    this._currentPolicy.addPages(page, pageSize);
  }

  public fetch(): void {
    this._currentPolicy
      .executeFetchPolicy(this.httpClient)
      .subscribe((response) => {
        this._directions = response;
      });
  }

  public setPolicy(policy: IFetchPolicy<EducationDirection[]>): void {
    this._currentPolicy = policy;
  }

  public get directions(): EducationDirection[] {
    return this._directions;
  }
}
