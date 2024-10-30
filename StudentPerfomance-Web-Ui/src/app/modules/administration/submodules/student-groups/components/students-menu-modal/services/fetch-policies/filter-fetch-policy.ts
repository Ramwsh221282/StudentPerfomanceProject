import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Student } from '../../../../../students/models/student.interface';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { StudentGroupPayloadBuilder } from '../../../../models/contracts/student-group-contract/student-group-payload-builder';
import { StudentPayloadBuilder } from '../../../../../students/models/contracts/student-contracts/student-payload-builder';
import { AuthService } from '../../../../../../../users/services/auth.service';
import { TokenPayloadBuilder } from '../../../../../../../../shared/models/common/token-contract/token-payload-builder';

export class FilterFetchPolicy implements IFetchPolicy<Student[]> {
  private readonly _apiUri: string;
  private readonly _payload: object;

  public constructor(
    student: Student,
    private readonly _authService: AuthService
  ) {
    this._apiUri = `${BASE_API_URI}/api/students/filter`;
    this._payload = {
      group: StudentGroupPayloadBuilder(student.group),
      student: StudentPayloadBuilder(student),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<Student[]> {
    const payload = this._payload;
    return httpClient.post<Student[]>(this._apiUri, payload);
  }

  public addPages(page: number, pageSize: number): void {}
}
