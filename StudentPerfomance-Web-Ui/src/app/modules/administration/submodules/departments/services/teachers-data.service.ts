import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IObservableFetchable } from '../../../../../shared/models/fetch-policices/iobservable-fetchable.interface';
import { Teacher } from '../../teachers/models/teacher.interface';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../shared/models/fetch-policices/fetch-policy-interface';

@Injectable({
  providedIn: 'any',
})
export class TeacherDataService implements IObservableFetchable<Teacher[]> {
  private readonly _httpClient: HttpClient;
  private _policy: IFetchPolicy<Teacher[]>;

  public constructor() {
    this._httpClient = inject(HttpClient);
  }

  public setPolicy(policy: IFetchPolicy<Teacher[]>): void {
    this._policy = policy;
  }

  public fetch(): Observable<Teacher[]> {
    return this._policy.executeFetchPolicy(this._httpClient);
  }
}
