import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { EducationPlan } from '../../../../education-plans/models/education-plan-interface';
import { Observable } from 'rxjs';
import { StudentGroupPayloadBuilder } from '../../../models/contracts/student-group-contract/student-group-payload-builder';
import { DirectionPayloadBuilder } from '../../../../education-directions/models/contracts/direction-payload-builder';
import { EducationPlanPayloadBuilder } from '../../../../education-plans/models/contracts/education-plan-contract/education-plan-payload-builder';
import { AuthService } from '../../../../../../users/services/auth.service';
import { TokenPayloadBuilder } from '../../../../../../../shared/models/common/token-contract/token-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class EducationPlanAttachmentService {
  private readonly _attachmentApiUri: string;
  private readonly _deattachmentApiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor(private readonly _authService: AuthService) {
    this._httpClient = inject(HttpClient);
    this._attachmentApiUri = `${BASE_API_URI}/api/student-groups/attach-education-plan`;
    this._deattachmentApiUri = `${BASE_API_URI}/api/student-groups/deattach-education-plan`;
  }

  public attachPlan(
    group: StudentGroup,
    plan: EducationPlan
  ): Observable<StudentGroup> {
    const body = this.buildAttachmentRequestBody(group, plan);
    return this._httpClient.put<StudentGroup>(this._attachmentApiUri, body);
  }

  public deattachPlan(group: StudentGroup): Observable<StudentGroup> {
    const body = this.buildDeattachmentRequestBody(group);
    return this._httpClient.put<StudentGroup>(this._deattachmentApiUri, body);
  }

  private buildAttachmentRequestBody(
    group: StudentGroup,
    plan: EducationPlan
  ): object {
    return {
      direction: DirectionPayloadBuilder(plan.direction),
      plan: EducationPlanPayloadBuilder(plan),
      group: StudentGroupPayloadBuilder(group),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }

  private buildDeattachmentRequestBody(group: StudentGroup): object {
    return {
      group: StudentGroupPayloadBuilder(group),
      token: TokenPayloadBuilder(this._authService.userData),
    };
  }
}
