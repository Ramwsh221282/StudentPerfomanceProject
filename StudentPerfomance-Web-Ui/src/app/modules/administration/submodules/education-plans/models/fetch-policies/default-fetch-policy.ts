import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { EducationPlan } from '../education-plan-interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { User } from '../../../../../users/services/user-interface';
import { AuthService } from '../../../../../users/services/auth.service';
import { PaginationPayloadBuilder } from '../../../../../../shared/models/common/pagination-contract/pagination-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../shared/models/common/token-contract/token-payload-builder';

export class DefaultFetchPolicy implements IFetchPolicy<EducationPlan[]> {
  private readonly _baseApiUri = `${BASE_API_URI}/api/education-plans/byPage`;
  private readonly _user: User;
  private payload: object;

  public constructor(private readonly authService: AuthService) {
    this._user = { ...authService.userData };
  }

  public executeFetchPolicy(
    httpClient: HttpClient
  ): Observable<EducationPlan[]> {
    const payload = this.payload;
    return httpClient.post<EducationPlan[]>(this._baseApiUri, payload);
  }

  public addPages(page: number, pageSize: number): void {
    this.payload = {
      pagination: PaginationPayloadBuilder(page, pageSize),
      token: TokenPayloadBuilder(this._user),
    };
  }
}
