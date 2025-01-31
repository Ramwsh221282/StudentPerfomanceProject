import { Injectable } from '@angular/core';
import { BaseHttpService } from '../../../../shared/models/common/base-http/base-http.service';
import { StudentGroup } from '../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { StudentGroupPayloadBuilder } from '../../../../modules/administration/submodules/student-groups/models/contracts/student-group-contract/student-group-payload-builder';
import { TokenPayloadBuilder } from '../../../../shared/models/common/token-contract/token-payload-builder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class DetachPlanGroupService extends BaseHttpService {
  public detach(group: StudentGroup): Observable<StudentGroup> {
    const url = `${this._config.baseApiUri}/api/student-groups/deattach-education-plan`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(group);
    return this._http.put<StudentGroup>(url, payload, { headers: headers });
  }

  private buildPayload(group: StudentGroup): object {
    return {
      group: StudentGroupPayloadBuilder(group),
      token: TokenPayloadBuilder(this._auth.userData),
    };
  }
}
