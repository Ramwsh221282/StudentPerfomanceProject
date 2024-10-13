import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Student } from '../../../../../students/models/student.interface';
import { StudentGroup } from '../../../../services/studentsGroup.interface';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';

export class DefaultFetchPolicy implements IFetchPolicy<Student[]> {
  private readonly _params: HttpParams;
  private readonly _apiUri: string;

  public constructor(group: StudentGroup) {
    this._apiUri = `${BASE_API_URI}/student/api/read/by-group`;
    this._params = new HttpParams()
      .set('Group.Name', group.name)
      .set('Group.EducationPlan.Year', group.plan.year)
      .set('Group.EducationPlan.Direction.Code', group.plan.direction.code)
      .set('Group.EducationPlan.Direction.Name', group.plan.direction.name)
      .set('Group.EducationPlan.Direction.Type', group.plan.direction.type);
  }

  executeFetchPolicy(httpClient: HttpClient): Observable<Student[]> {
    const params = this._params;
    return httpClient.get<Student[]>(this._apiUri, { params });
  }

  addPages(page: number, pageSize: number): void {}
}
