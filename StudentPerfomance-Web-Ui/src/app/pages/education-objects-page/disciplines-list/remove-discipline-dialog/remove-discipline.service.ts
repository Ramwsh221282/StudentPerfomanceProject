import { Injectable } from '@angular/core';
import { DirectionPayloadBuilder } from '../../../../modules/administration/submodules/education-directions/models/contracts/direction-payload-builder';
import { SemesterPlan } from '../../../../modules/administration/submodules/semester-plans/models/semester-plan.interface';
import { Semester } from '../../../../modules/administration/submodules/semesters/models/semester.interface';
import { EducationPlan } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { EducationPlanPayloadBuilder } from '../../../../modules/administration/submodules/education-plans/models/contracts/education-plan-contract/education-plan-payload-builder';
import { SemesterPayloadBuilder } from '../../../../modules/administration/submodules/education-plans/models/contracts/semester-contract/semester-payload-builder';
import { TokenPayloadBuilder } from '../../../../shared/models/common/token-contract/token-payload-builder';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../../../../shared/models/common/base-http/base-http.service';
import { SemesterPlanPayloadBuilder } from '../create-discipline-dialog/semester-plan-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class RemoveDisciplineService extends BaseHttpService {
  public remove(
    discipline: SemesterPlan,
    semester: Semester,
    plan: EducationPlan,
    direction: EducationDirection,
  ): Observable<SemesterPlan> {
    const url = `${this._config.baseApiUri}/api/semester-plans`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(discipline, semester, plan, direction);
    return this._http.delete<SemesterPlan>(url, {
      headers: headers,
      body: payload,
    });
  }

  private buildPayload(
    discipline: SemesterPlan,
    semester: Semester,
    plan: EducationPlan,
    direction: EducationDirection,
  ): object {
    return {
      direction: DirectionPayloadBuilder(direction),
      plan: EducationPlanPayloadBuilder(plan),
      semester: SemesterPayloadBuilder(semester),
      discipline: SemesterPlanPayloadBuilder(discipline),
      token: TokenPayloadBuilder(this._auth.userData),
    };
  }
}
