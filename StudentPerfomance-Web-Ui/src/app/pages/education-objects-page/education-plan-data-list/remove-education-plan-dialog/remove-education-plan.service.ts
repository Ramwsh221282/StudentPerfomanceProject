import { Injectable } from '@angular/core';
import { AuthService } from '../../../user-page/services/auth.service';
import { AppConfigService } from '../../../../app.config.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EducationPlan } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { DirectionPayloadBuilder } from '../../../../modules/administration/submodules/education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../../../../modules/administration/submodules/education-plans/models/contracts/education-plan-contract/education-plan-payload-builder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class RemoveEducationPlanService {
  public constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _config: AppConfigService,
  ) {}

  public delete(
    plan: EducationPlan,
    direction: EducationDirection,
  ): Observable<EducationPlan> {
    const url = `${this._config.baseApiUri}/api/education-plans`;
    const headers = this.buildHttpHeaders();
    const body = this.buildPayload(plan, direction);
    return this._http.delete<EducationPlan>(url, {
      headers: headers,
      body,
    });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
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
