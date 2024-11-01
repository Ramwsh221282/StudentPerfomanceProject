import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { EducationPlan } from '../../../models/education-plan-interface';
import { Observable } from 'rxjs';
import { Semester } from '../../../../semesters/models/semester.interface';
import { User } from '../../../../../../users/services/user-interface';
import { AuthService } from '../../../../../../users/services/auth.service';
import { DirectionPayloadBuilder } from '../../../../education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../../../models/contracts/education-plan-contract/education-plan-payload-builder';
import { TokenPayloadBuilder } from '../../../../../../../shared/models/common/token-contract/token-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class EducationPlanSemestersService {
  private readonly _user: User;
  private readonly _httpClient: HttpClient;
  private readonly _apiUri: string = `${BASE_API_URI}/api/semesters/by-education-plan`;

  public constructor() {
    this._httpClient = inject(HttpClient);
    const authService = inject(AuthService);
    this._user = { ...authService.userData };
  }

  public getPlanSemesters(plan: EducationPlan): Observable<Semester[]> {
    const payload = this.buildPayload(plan);
    return this._httpClient.post<Semester[]>(this._apiUri, payload);
  }

  private buildPayload(plan: EducationPlan): object {
    return {
      direction: DirectionPayloadBuilder(plan.direction),
      plan: EducationPlanPayloadBuilder(plan),
      token: TokenPayloadBuilder(this._user),
    };
  }
}
