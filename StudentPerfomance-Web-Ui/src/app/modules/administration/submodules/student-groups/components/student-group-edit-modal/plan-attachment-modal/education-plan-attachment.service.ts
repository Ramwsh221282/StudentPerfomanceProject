import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { EducationPlan } from '../../../../education-plans/models/education-plan-interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class EducationPlanAttachmentService {
  private readonly _attachmentApiUri: string;
  private readonly _deattachmentApiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor() {
    this._httpClient = inject(HttpClient);
    this._attachmentApiUri = `${BASE_API_URI}/student-groups/api/management/plan-attachment`;
    this._deattachmentApiUri = `${BASE_API_URI}/student-groups/api/management/plan-deattachment`;
  }

  public attachPlan(
    group: StudentGroup,
    plan: EducationPlan
  ): Observable<StudentGroup> {
    const body = this.buildRequestBody(group, plan);
    return this._httpClient.put<StudentGroup>(this._attachmentApiUri, body);
  }

  public deattachPlan(group: StudentGroup): Observable<StudentGroup> {
    const body = this.buildDeattachmentRequestBody(group);
    return this._httpClient.put<StudentGroup>(this._deattachmentApiUri, body);
  }

  private buildRequestBody(group: StudentGroup, plan: EducationPlan): object {
    return {
      group: {
        name: group.name,
      },
      plan: {
        year: plan.year,
        direction: {
          code: plan.direction.code,
          name: plan.direction.name,
          type: plan.direction.type,
        },
      },
    };
  }

  private buildDeattachmentRequestBody(group: StudentGroup): object {
    return {
      group: {
        name: group.name,
      },
    };
  }
}
