import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
//import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { SemesterPlan } from '../../../../semester-plans/models/semester-plan.interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../../../../pages/user-page/services/auth.service';
import { DirectionPayloadBuilder } from '../../../../education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../../../models/contracts/education-plan-contract/education-plan-payload-builder';
import { SemesterPayloadBuilder } from '../../../models/contracts/semester-contract/semester-payload-builder';
import { SemesterPlanPayloadBuilder } from './semester-plan-payload.builder';
import { TokenPayloadBuilder } from '../../../../../../../shared/models/common/token-contract/token-payload-builder';
import { AppConfigService } from '../../../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class SemesterDisciplinesCreationService {
  private readonly _apiUri: string;

  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    //this._apiUri = `${BASE_API_URI}/api/semester-plans`;
    this._apiUri = `${this._appConfig.baseApiUri}/api/semester-plans`;
  }

  public create(plan: SemesterPlan): Observable<SemesterPlan> {
    const body = this.buildRequestBody(plan);
    const headers = this.buildHttpHeaders();
    return this._httpClient.post<SemesterPlan>(this._apiUri, body, {
      headers: headers,
    });
  }

  private buildRequestBody(plan: SemesterPlan): object {
    return {
      direction: DirectionPayloadBuilder(plan.semester.educationPlan.direction),
      plan: EducationPlanPayloadBuilder(plan.semester.educationPlan),
      semester: SemesterPayloadBuilder(plan.semester),
      discipline: SemesterPlanPayloadBuilder(plan),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
