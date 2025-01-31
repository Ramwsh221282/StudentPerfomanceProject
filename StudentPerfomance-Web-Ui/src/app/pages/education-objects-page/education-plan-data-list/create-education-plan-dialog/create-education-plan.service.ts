import { Injectable } from '@angular/core';
import { EducationPlan } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { DirectionPayloadBuilder } from '../../../../modules/administration/submodules/education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../../../../modules/administration/submodules/education-plans/models/contracts/education-plan-contract/education-plan-payload-builder';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../../../../shared/models/common/base-http/base-http.service';

@Injectable({
  providedIn: 'any',
})
export class CreateEducationPlanService extends BaseHttpService {
  public create(
    plan: EducationPlan,
    direction: EducationDirection,
  ): Observable<EducationPlan> {
    const url = `${this._config.baseApiUri}/api/education-plans`;
    const headers = this.buildHttpHeaders();
    const body = this.buildPayload(plan, direction);
    return this._http.post<EducationPlan>(url, body, {
      headers: headers,
    });
  }

  private buildPayload(
    plan: EducationPlan,
    direction: EducationDirection,
  ): object {
    return {
      direction: DirectionPayloadBuilder(direction),
      plan: EducationPlanPayloadBuilder(plan),
    };
  }
}
