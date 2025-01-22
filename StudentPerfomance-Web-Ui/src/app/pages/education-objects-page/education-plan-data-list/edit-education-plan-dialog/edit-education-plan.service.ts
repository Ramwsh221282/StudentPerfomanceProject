import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppConfigService } from '../../../../app.config.service';
import { AuthService } from '../../../user-page/services/auth.service';
import { EducationPlan } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { DirectionPayloadBuilder } from '../../../../modules/administration/submodules/education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../../../../modules/administration/submodules/education-plans/models/contracts/education-plan-contract/education-plan-payload-builder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class EditEducationPlanService {
  constructor(
    private readonly _http: HttpClient,
    private readonly _config: AppConfigService,
    private readonly _auth: AuthService,
  ) {}

  public edit(
    initial: EducationPlan,
    updated: EducationPlan,
    direction: EducationDirection,
  ): Observable<EducationPlan> {
    const url = `${this._config.baseApiUri}/api/education-plans`;
    const headers = this.buildHeaders();
    const payload = this.buildPayload(initial, updated, direction);
    return this._http.put<EducationPlan>(url, payload, { headers: headers });
  }

  private buildHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
  }

  private buildPayload(
    initial: EducationPlan,
    updated: EducationPlan,
    direction: EducationDirection,
  ): object {
    return {
      direction: DirectionPayloadBuilder(direction),
      initial: EducationPlanPayloadBuilder(initial),
      updated: EducationPlanPayloadBuilder(updated),
    };
  }
}
