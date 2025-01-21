import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
//import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { StudentGroup } from './studentsGroup.interface';
import { EducationPlan } from '../../education-plans/models/education-plan-interface';
import { Observable } from 'rxjs';
import { StudentGroupPayloadBuilder } from '../models/contracts/student-group-contract/student-group-payload-builder';
import { DirectionPayloadBuilder } from '../../education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../../education-plans/models/contracts/education-plan-contract/education-plan-payload-builder';
import { AuthService } from '../../../../../pages/user-page/services/auth.service';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';
import { AppConfigService } from '../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class EducationPlanAttachmentService {
  private readonly _attachmentApiUri: string;
  private readonly _deattachmentApiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor(
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    this._httpClient = inject(HttpClient);
    //this._attachmentApiUri = `${BASE_API_URI}/api/student-groups/attach-education-plan`;
    //this._deattachmentApiUri = `${BASE_API_URI}/api/student-groups/deattach-education-plan`;
    this._attachmentApiUri = `${this._appConfig.baseApiUri}/api/student-groups/attach-education-plan`;
    this._deattachmentApiUri = `${this._appConfig.baseApiUri}/api/student-groups/deattach-education-plan`;
  }

  public attachPlan(
    group: StudentGroup,
    plan: EducationPlan,
    semesterNumber: number,
  ): Observable<StudentGroup> {
    const body = this.buildAttachmentRequestBody(group, plan, semesterNumber);
    const headers = this.buildHttpHeaders();
    return this._httpClient.put<StudentGroup>(this._attachmentApiUri, body, {
      headers: headers,
    });
  }

  public deattachPlan(group: StudentGroup): Observable<StudentGroup> {
    const body = this.buildDeattachmentRequestBody(group);
    const headers = this.buildHttpHeaders();
    return this._httpClient.put<StudentGroup>(this._deattachmentApiUri, body, {
      headers: headers,
    });
  }

  private buildAttachmentRequestBody(
    group: StudentGroup,
    plan: EducationPlan,
    semesterNumber: number,
  ): object {
    return {
      direction: DirectionPayloadBuilder(plan.direction),
      plan: EducationPlanPayloadBuilder(plan),
      group: StudentGroupPayloadBuilder(group),
      semester: {
        number: semesterNumber,
      },
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }

  private buildDeattachmentRequestBody(group: StudentGroup): object {
    return {
      group: StudentGroupPayloadBuilder(group),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
