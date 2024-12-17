import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { IFetchable } from '../../../../../shared/models/fetch-policices/ifetchable-interface';
import { IFetchPolicy } from '../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { DefaultFetchPolicy } from '../models/fetch-policies/default-fetch-policy';
import { AuthService } from '../../../../users/services/auth.service';
import { AppConfigService } from '../../../../../app.config.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupsFetchDataService
  extends StudentGroupsService
  implements IFetchable<StudentGroup[]>
{
  private _currentPolicy: IFetchPolicy<StudentGroup[]>;

  public constructor(
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    super();
    this._currentPolicy = new DefaultFetchPolicy(
      this._authService,
      this._appConfig,
    );
  }

  public setPolicy(policy: IFetchPolicy<StudentGroup[]>): void {
    this._currentPolicy = policy;
  }

  public fetch(): Observable<StudentGroup[]> {
    return this._currentPolicy.executeFetchPolicy(this.httpClient);
  }

  public addPages(page: number, pageSize: number): void {
    this._currentPolicy.addPages(page, pageSize);
  }
}
