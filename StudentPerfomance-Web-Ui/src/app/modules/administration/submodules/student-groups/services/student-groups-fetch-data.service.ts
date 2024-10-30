import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { IFetchable } from '../../../../../shared/models/fetch-policices/ifetchable-interface';
import { IFetchPolicy } from '../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { DefaultFetchPolicy } from '../models/fetch-policies/default-fetch-policy';
import { AuthService } from '../../../../users/services/auth.service';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupsFetchDataService
  extends StudentGroupsService
  implements IFetchable<StudentGroup[]>
{
  private _currentPolicy: IFetchPolicy<StudentGroup[]>;
  private _groups: StudentGroup[];

  public constructor(private readonly _authService: AuthService) {
    super();
    this._groups = [];
    this._currentPolicy = new DefaultFetchPolicy(this._authService);
  }

  public setPolicy(policy: IFetchPolicy<StudentGroup[]>): void {
    this._currentPolicy = policy;
  }

  public fetch(): void {
    this._currentPolicy
      .executeFetchPolicy(this.httpClient)
      .subscribe((response) => {
        this._groups = response;
      });
  }

  public addPages(page: number, pageSize: number): void {
    this._currentPolicy.addPages(page, pageSize);
  }

  public get groups(): StudentGroup[] {
    return this._groups;
  }
}
