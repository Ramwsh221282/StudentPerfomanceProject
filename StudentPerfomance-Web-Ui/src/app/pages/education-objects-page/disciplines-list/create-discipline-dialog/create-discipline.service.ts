import { Injectable } from '@angular/core';
import { SemesterPlan } from '../../../../modules/administration/submodules/semester-plans/models/semester-plan.interface';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { EducationPlan } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { DirectionPayloadBuilder } from '../../../../modules/administration/submodules/education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../../../../modules/administration/submodules/education-plans/models/contracts/education-plan-contract/education-plan-payload-builder';
import { Semester } from '../../../../modules/administration/submodules/semesters/models/semester.interface';
import { SemesterPayloadBuilder } from '../../../../modules/administration/submodules/education-plans/models/contracts/semester-contract/semester-payload-builder';
import { TokenPayloadBuilder } from '../../../../shared/models/common/token-contract/token-payload-builder';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../../../../shared/models/common/base-http/base-http.service';
import { SemesterPlanPayloadBuilder } from './semester-plan-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class CreateDisciplineService extends BaseHttpService {
  public create(
    discipline: SemesterPlan,
    semester: Semester,
    educationPlan: EducationPlan,
    direction: EducationDirection,
  ): Observable<SemesterPlan> {
    const url = `${this._config.baseApiUri}/api/semester-plans`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildRequestBody(
      discipline,
      semester,
      educationPlan,
      direction,
    );
    return this._http.post<SemesterPlan>(url, payload, { headers: headers });
  }

  private buildRequestBody(
    discipline: SemesterPlan,
    semester: Semester,
    educationPlan: EducationPlan,
    direction: EducationDirection,
  ): object {
    return {
      direction: DirectionPayloadBuilder(direction),
      plan: EducationPlanPayloadBuilder(educationPlan),
      semester: SemesterPayloadBuilder(semester),
      discipline: SemesterPlanPayloadBuilder(discipline),
      token: TokenPayloadBuilder(this._auth.userData),
    };
  }
}
