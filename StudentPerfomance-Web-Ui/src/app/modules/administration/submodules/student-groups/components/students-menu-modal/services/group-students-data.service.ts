import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { IFetchPolicy } from '../../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { IObservableFetchable } from '../../../../../../../shared/models/fetch-policices/iobservable-fetchable.interface';
import { Observable } from 'rxjs';
import { Student } from '../../../../students/models/student.interface';
import { DefaultFetchPolicy } from './fetch-policies/default-fetch-policy';

@Injectable({
  providedIn: 'any',
})
export class groupStudentsDataService
  implements IObservableFetchable<Student[]>
{
  private readonly _httpClient: HttpClient;
  private _policy: IFetchPolicy<Student[]>;

  public constructor() {
    this._httpClient = inject(HttpClient);
  }

  public fetch(): Observable<Student[]> {
    return this._policy.executeFetchPolicy(this._httpClient);
  }

  public initialize(group: StudentGroup): void {
    this._policy = new DefaultFetchPolicy(group);
  }

  public setPolicy(policy: IFetchPolicy<Student[]>): void {
    this._policy = policy;
  }
}
