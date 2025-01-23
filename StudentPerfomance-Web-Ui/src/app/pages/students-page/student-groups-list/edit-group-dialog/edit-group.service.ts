import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppConfigService } from '../../../../app.config.service';
import { AuthService } from '../../../user-page/services/auth.service';
import { StudentGroup } from '../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { StudentGroupPayloadBuilder } from '../../../../modules/administration/submodules/student-groups/models/contracts/student-group-contract/student-group-payload-builder';
import { TokenPayloadBuilder } from '../../../../shared/models/common/token-contract/token-payload-builder';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'any' })
export class EditGroupService {
  public constructor(
    private readonly _http: HttpClient,
    private readonly _config: AppConfigService,
    private readonly _auth: AuthService,
  ) {}

  public edit(
    initial: StudentGroup,
    updated: StudentGroup,
  ): Observable<StudentGroup> {
    const url = `${this._config.baseApiUri}/api/student-groups`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(initial, updated);
    return this._http.put<StudentGroup>(url, payload, { headers: headers });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
  }

  private buildPayload(initial: StudentGroup, updated: StudentGroup): object {
    return {
      initial: StudentGroupPayloadBuilder(initial),
      updated: StudentGroupPayloadBuilder(updated),
      token: TokenPayloadBuilder(this._auth.userData),
    };
  }
}
