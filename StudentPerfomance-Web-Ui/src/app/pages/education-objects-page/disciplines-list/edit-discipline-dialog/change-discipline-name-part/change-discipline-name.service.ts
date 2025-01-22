import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../../../user-page/services/auth.service';
import { AppConfigService } from '../../../../../app.config.service';
import { SemesterPlan } from '../../../../../modules/administration/submodules/semester-plans/models/semester-plan.interface';
import { Semester } from '../../../../../modules/administration/submodules/semesters/models/semester.interface';
import { EducationPlan } from '../../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { SemesterPlanPayloadBuilder } from '../../../../../modules/administration/submodules/education-plans/components/education-plans-table/education-plan-item-workspace/semester-plan-payload.builder';
import { DirectionPayloadBuilder } from '../../../../../modules/administration/submodules/education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../../../../../modules/administration/submodules/education-plans/models/contracts/education-plan-contract/education-plan-payload-builder';
import { SemesterPayloadBuilder } from '../../../../../modules/administration/submodules/education-plans/models/contracts/semester-contract/semester-payload-builder';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class ChangeDisciplineNameService {
  public constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _config: AppConfigService,
  ) {}

  public changeName(
    initial: SemesterPlan,
    updated: SemesterPlan,
    semester: Semester,
    educationPlan: EducationPlan,
    direction: EducationDirection,
  ): Observable<SemesterPlan> {
    const url = `${this._config.baseApiUri}/api/semester-plans`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(
      initial,
      updated,
      semester,
      educationPlan,
      direction,
    );
    return this._http.put<SemesterPlan>(url, payload, { headers: headers });
  }

  private buildPayload(
    initial: SemesterPlan,
    updated: SemesterPlan,
    semester: Semester,
    educationPlan: EducationPlan,
    direction: EducationDirection,
  ): object {
    return {
      initial: SemesterPlanPayloadBuilder(initial),
      updated: SemesterPlanPayloadBuilder(updated),
      semester: SemesterPayloadBuilder(semester),
      plan: EducationPlanPayloadBuilder(educationPlan),
      direction: DirectionPayloadBuilder(direction),
      token: TokenPayloadBuilder(this._auth.userData),
    };
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
  }
}
