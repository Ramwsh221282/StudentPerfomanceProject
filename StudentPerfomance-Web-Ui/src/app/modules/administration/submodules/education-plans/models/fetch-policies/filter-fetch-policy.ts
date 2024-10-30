import { HttpClient, HttpParams } from '@angular/common/http';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { EducationPlan } from '../education-plan-interface';
import { Observable } from 'rxjs';
import { User } from '../../../../../users/services/user-interface';
import { AuthService } from '../../../../../users/services/auth.service';
import { DirectionPayloadBuilder } from '../../../education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../contracts/education-plan-contract/education-plan-payload-builder';
import { PaginationPayloadBuilder } from '../../../../../../shared/models/common/pagination-contract/pagination-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../shared/models/common/token-contract/token-payload-builder';

export class FilterFetchPolicy implements IFetchPolicy<EducationPlan[]> {
  private readonly _baseApiUri = `${BASE_API_URI}/api/education-plans/filter`;
  private readonly _plan: EducationPlan;
  private readonly _user: User;
  private _payload: object;

  public constructor(plan: EducationPlan, authService: AuthService) {
    this._user = { ...authService.userData };
    this._plan = plan;
  }

  public executeFetchPolicy(
    httpClient: HttpClient
  ): Observable<EducationPlan[]> {
    const payload = this._payload;
    return httpClient.post<EducationPlan[]>(this._baseApiUri, payload);
  }

  public addPages(page: number, pageSize: number): void {
    this._payload = {
      direction: DirectionPayloadBuilder(this._plan.direction),
      plan: EducationPlanPayloadBuilder(this._plan),
      pagination: PaginationPayloadBuilder(page, pageSize),
      token: TokenPayloadBuilder(this._user),
    };
  }
}
