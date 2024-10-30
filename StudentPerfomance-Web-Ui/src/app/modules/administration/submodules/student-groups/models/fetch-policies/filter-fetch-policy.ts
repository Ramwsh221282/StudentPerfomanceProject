import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../../users/services/auth.service';
import { StudentGroupPayloadBuilder } from '../contracts/student-group-contract/student-group-payload-builder';
import { PaginationPayloadBuilder } from '../../../../../../shared/models/common/pagination-contract/pagination-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../shared/models/common/token-contract/token-payload-builder';

export class FilterFetchPolicy implements IFetchPolicy<StudentGroup[]> {
  private readonly _apiUri: string = `${BASE_API_URI}/api/student-groups/filter`;
  private readonly _group: StudentGroup;
  private _payload: object;

  public constructor(
    group: StudentGroup,
    private readonly authService: AuthService
  ) {
    this._group = group;
  }

  public executeFetchPolicy(
    httpClient: HttpClient
  ): Observable<StudentGroup[]> {
    const payload = this._payload;
    return httpClient.post<StudentGroup[]>(this._apiUri, payload);
  }

  public addPages(page: number, pageSize: number): void {
    this._payload = {
      group: StudentGroupPayloadBuilder(this._group),
      pagination: PaginationPayloadBuilder(page, pageSize),
      token: TokenPayloadBuilder(this.authService.userData),
    };
  }
}
