import { HttpClient, HttpParams } from '@angular/common/http';
import { IFetchPolicy } from '../../../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Student } from '../../../../../students/models/student.interface';
import { Observable } from 'rxjs';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { StudentGroup } from '../../../../services/studentsGroup.interface';

export class ActiveOnlyFetchPolicy implements IFetchPolicy<Student[]> {
  private readonly _apiUri: string;
  private readonly _httpParams: HttpParams;

  public constructor(group: StudentGroup) {
    this._apiUri = `${BASE_API_URI}/student/api/read/search`;
    this._httpParams = new HttpParams()
      .set('Student.Name', '')
      .set('Student.Surname', '')
      .set('Student.Thirdname', '')
      .set('Student.State', 'Активен')
      .set('Student.Recordbook', 0)
      .set('Student.Group.Name', group.name)
      .set('Student.Group.EducationPlan.Year', group.plan.year)
      .set(
        'Student.Group.EducationPlan.Direction.EntityNumber',
        group.plan.direction.entityNumber
      )
      .set(
        'Student.Group.EducationPlan.Direction.Code',
        group.plan.direction.code
      )
      .set(
        'Student.Group.EducationPlan.Direction.Name',
        group.plan.direction.name
      )
      .set(
        'Student.Group.EducationPlan.Direction.Type',
        group.plan.direction.type
      );
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<Student[]> {
    const params = this._httpParams;
    return httpClient.get<Student[]>(this._apiUri, { params });
  }

  public addPages(page: number, pageSize: number): void {}
}
