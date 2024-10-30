import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { Semester } from '../../../../../semesters/models/semester.interface';
import { SemesterPlan } from '../../../../../semester-plans/models/semester-plan.interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../../../../users/services/auth.service';
import { DirectionPayloadBuilder } from '../../../../../education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../../../../models/contracts/education-plan-contract/education-plan-payload-builder';
import { SemesterPayloadBuilder } from '../../../../models/contracts/semester-contract/semester-payload-builder';
import { SemesterPlanPayloadBuilder } from './models/contracts/semester-plan-contract/semester-plan-payload.builder';
import { TokenPayloadBuilder } from '../../../../../../../../shared/models/common/token-contract/token-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class SemesterDisciplinesCreationService {
  private readonly _httpClient: HttpClient;
  private readonly _apiUri: string = `${BASE_API_URI}/api/semester-plans`;

  public constructor(private readonly _authService: AuthService) {
    this._httpClient = inject(HttpClient);
  }

  public create(plan: SemesterPlan): Observable<SemesterPlan> {
    const body = this.buildRequestBody(plan);
    return this._httpClient.post<SemesterPlan>(this._apiUri, body);
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
}
