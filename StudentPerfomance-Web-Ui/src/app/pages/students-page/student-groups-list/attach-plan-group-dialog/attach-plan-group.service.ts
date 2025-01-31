import { Injectable } from '@angular/core';
import { BaseHttpService } from '../../../../shared/models/common/base-http/base-http.service';
import { StudentGroup } from '../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { EducationPlan } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { DirectionPayloadBuilder } from '../../../../modules/administration/submodules/education-directions/models/contracts/direction-payload-builder';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { EducationPlanPayloadBuilder } from '../../../../modules/administration/submodules/education-plans/models/contracts/education-plan-contract/education-plan-payload-builder';
import { StudentGroupPayloadBuilder } from '../../../../modules/administration/submodules/student-groups/models/contracts/student-group-contract/student-group-payload-builder';
import { TokenPayloadBuilder } from '../../../../shared/models/common/token-contract/token-payload-builder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class AttachPlanGroupService extends BaseHttpService {
  public attach(
    group: StudentGroup,
    direction: EducationDirection,
    plan: EducationPlan,
    semesterNumber: number,
  ): Observable<StudentGroup> {
    const url = `${this._config.baseApiUri}/api/student-groups/attach-education-plan`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(group, direction, plan, semesterNumber);
    return this._http.put<StudentGroup>(url, payload, { headers: headers });
  }

  private buildPayload(
    group: StudentGroup,
    direction: EducationDirection,
    plan: EducationPlan,
    semesterNumber: number,
  ): object {
    return {
      direction: DirectionPayloadBuilder(direction),
      plan: EducationPlanPayloadBuilder(plan),
      group: StudentGroupPayloadBuilder(group),
      semester: {
        number: semesterNumber,
      },
      token: TokenPayloadBuilder(this._auth.userData),
    };
  }
}
