import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { SemesterPlan } from '../../../../../semester-plans/models/semester-plan.interface';
import { Semester } from '../../../../../semesters/models/semester.interface';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../../../../users/services/auth.service';
import { DirectionPayloadBuilder } from '../../../../../education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../../../../models/contracts/education-plan-contract/education-plan-payload-builder';
import { SemesterPayloadBuilder } from '../../../../models/contracts/semester-contract/semester-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../../../shared/models/common/token-contract/token-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class SemesterDisciplinesDataService {
  private readonly _httpClient: HttpClient;
  private readonly _apiUri: string = `${BASE_API_URI}/api/semester-plans/get-by-semester`;

  public constructor(private readonly _authService: AuthService) {
    this._httpClient = inject(HttpClient);
  }

  public getSemesterDisciplines(
    semester: Semester
  ): Observable<SemesterPlan[]> {
    const payload = this.buildPayload(semester);
    return this._httpClient.post<SemesterPlan[]>(this._apiUri, payload);
  }

  private buildPayload(semester: Semester): object {
    return {
      direction: DirectionPayloadBuilder(semester.educationPlan.direction),
      plan: EducationPlanPayloadBuilder(semester.educationPlan),
      semester: SemesterPayloadBuilder(semester),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
