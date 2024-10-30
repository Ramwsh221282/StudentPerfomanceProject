import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../../users/services/auth.service';
import { PaginationPayloadBuilder } from '../../../../../../shared/models/common/pagination-contract/pagination-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../shared/models/common/token-contract/token-payload-builder';

export class DefaultFetchPolicy implements IFetchPolicy<StudentGroup[]> {
  private readonly _apiUri: string = `${BASE_API_URI}/api/student-groups/byPage`;
  private _payload: object;

  public constructor(private readonly _authService: AuthService) {}

  public executeFetchPolicy(
    httpClient: HttpClient
  ): Observable<StudentGroup[]> {
    const payload = this._payload;
    return httpClient.post<StudentGroup[]>(this._apiUri, payload);
  }

  public addPages(page: number, pageSize: number): void {
    this._payload = {
      pagination: PaginationPayloadBuilder(page, pageSize),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
