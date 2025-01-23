import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../../user-page/services/auth.service';
import { AppConfigService } from '../../../../app.config.service';
import { StudentGroup } from '../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { TokenPayloadBuilder } from '../../../../shared/models/common/token-contract/token-payload-builder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class CreateGroupService {
  public constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _config: AppConfigService,
  ) {}

  public create(group: StudentGroup): Observable<StudentGroup> {
    const url = `${this._config.baseApiUri}/api/student-groups`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(group);
    return this._http.post<StudentGroup>(url, payload, { headers: headers });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
  }

  private buildPayload(group: StudentGroup): object {
    return {
      group: {
        name: group.name,
      },
      token: TokenPayloadBuilder(this._auth.userData),
    };
  }
}
