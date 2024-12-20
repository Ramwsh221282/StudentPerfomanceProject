import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IObservableFetchable } from '../../../../../shared/models/fetch-policices/iobservable-fetchable.interface';
import { AssignmentSession } from '../models/assignment-session-interface';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../shared/models/fetch-policices/fetch-policy-interface';

@Injectable({
  providedIn: 'any',
})
export class AssignmentSessionDataService
  implements IObservableFetchable<AssignmentSession[]>
{
  private _policy: IFetchPolicy<AssignmentSession[]>;

  public constructor(private readonly _httpClient: HttpClient) {}

  public setPolicy(policy: IFetchPolicy<AssignmentSession[]>): void {
    this._policy = policy;
  }

  public fetch(): Observable<AssignmentSession[]> {
    return this._policy.executeFetchPolicy(this._httpClient);
  }
}
