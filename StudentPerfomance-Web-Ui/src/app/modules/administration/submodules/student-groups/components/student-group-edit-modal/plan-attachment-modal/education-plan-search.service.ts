import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { Observable } from 'rxjs';
import { EducationPlan } from '../../../../education-plans/models/education-plan-interface';
import { AuthService } from '../../../../../../users/services/auth.service';
import { TokenPayloadBuilder } from '../../../../../../../shared/models/common/token-contract/token-payload-builder';
import { DirectionPayloadBuilder } from '../../../../education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../../../../education-plans/models/contracts/education-plan-contract/education-plan-payload-builder';
import { EducationDirection } from '../../../../education-directions/models/education-direction-interface';

@Injectable({
  providedIn: 'any',
})
export class EducationPlanSearchService {
  private readonly _httpClient: HttpClient;

  public constructor(private readonly _authService: AuthService) {
    this._httpClient = inject(HttpClient);
  }

  public getAll(): Observable<EducationPlan[]> {
    const payload: object = {
      token: TokenPayloadBuilder(this._authService.userData),
    };
    const apiUri: string = `${BASE_API_URI}/api/education-plans/all`;
    return this._httpClient.post<EducationPlan[]>(apiUri, payload);
  }

  public search(plan: EducationPlan): Observable<EducationPlan[]> {
    const payload: object = {
      direction: DirectionPayloadBuilder(plan.direction),
      plan: EducationPlanPayloadBuilder(plan),
      token: TokenPayloadBuilder(this._authService.userData),
    };
    const apiUri: string = `${BASE_API_URI}/api/education-plans/search`;
    return this._httpClient.post<EducationPlan[]>(apiUri, payload);
  }

  public getByDirection(
    direction: EducationDirection
  ): Observable<EducationPlan[]> {
    const apiUri: string = `${BASE_API_URI}/api/education-plans/by-education-direction`;
    const payload: object = {
      direction: DirectionPayloadBuilder(direction),
      token: TokenPayloadBuilder(this._authService.userData),
    };
    return this._httpClient.post<EducationPlan[]>(apiUri, payload);
  }
}
