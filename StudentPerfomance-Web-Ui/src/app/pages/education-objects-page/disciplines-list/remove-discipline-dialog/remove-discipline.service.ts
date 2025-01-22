import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../../user-page/services/auth.service';
import { AppConfigService } from '../../../../app.config.service';
import { DirectionPayloadBuilder } from '../../../../modules/administration/submodules/education-directions/models/contracts/direction-payload-builder';
import { SemesterPlan } from '../../../../modules/administration/submodules/semester-plans/models/semester-plan.interface';
import { Semester } from '../../../../modules/administration/submodules/semesters/models/semester.interface';
import { EducationPlan } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { EducationPlanPayloadBuilder } from '../../../../modules/administration/submodules/education-plans/models/contracts/education-plan-contract/education-plan-payload-builder';
import { SemesterPayloadBuilder } from '../../../../modules/administration/submodules/education-plans/models/contracts/semester-contract/semester-payload-builder';
import { SemesterPlanPayloadBuilder } from '../../../../modules/administration/submodules/education-plans/components/education-plans-table/education-plan-item-workspace/semester-plan-payload.builder';
import { TokenPayloadBuilder } from '../../../../shared/models/common/token-contract/token-payload-builder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class RemoveDisciplineService {
  public constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _config: AppConfigService,
  ) {}

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

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
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
