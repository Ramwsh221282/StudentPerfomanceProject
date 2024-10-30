import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { EducationDirection } from '../education-direction-interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../../users/services/auth.service';
import { PaginationPayloadBuilder } from '../../../../../../shared/models/common/pagination-contract/pagination-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../shared/models/common/token-contract/token-payload-builder';
import { DirectionPayloadBuilder } from '../contracts/direction-payload-builder';

export class FilterFetchPolicy implements IFetchPolicy<EducationDirection[]> {
  private readonly _baseApiUri = `${BASE_API_URI}/api/education-direction/filter`;
  private readonly _direction: EducationDirection;
  private payload: object;

  public constructor(
    direction: EducationDirection,
    private readonly authService: AuthService
  ) {
    this._direction = direction;
  }

  public executeFetchPolicy(
    httpClient: HttpClient
  ): Observable<EducationDirection[]> {
    const payload = this.payload;
    return httpClient.post<EducationDirection[]>(this._baseApiUri, payload);
  }

  public addPages(page: number, pageSize: number): void {
    this.payload = {
      direction: DirectionPayloadBuilder(this._direction),
      pagination: PaginationPayloadBuilder(page, pageSize),
      token: TokenPayloadBuilder(this.authService.userData),
    };
  }
}
