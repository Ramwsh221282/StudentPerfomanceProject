import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Student } from '../../../../../students/models/student.interface';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';

export class FilterFetchPolicy implements IFetchPolicy<Student[]> {
  private readonly _apiUri: string;
  private readonly _httpParams: HttpParams;

  public constructor(student: Student) {
    this._apiUri = `${BASE_API_URI}/student/api/read/search`;
    this._httpParams = new HttpParams();
    this._httpParams = new HttpParams()
      .set('Student.Name', student.name)
      .set('Student.Surname', student.surname)
      .set('Student.Thirdname', student.thirdname)
      .set('Student.State', student.state)
      .set('Student.Recordbook', student.recordbook)
      .set('Student.Group.Name', student.group.name)
      .set('Student.Group.EducationPlan.Year', student.group.plan.year)
      .set(
        'Student.Group.EducationPlan.Direction.EntityNumber',
        student.group.plan.direction.entityNumber
      )
      .set(
        'Student.Group.EducationPlan.Direction.Code',
        student.group.plan.direction.code
      )
      .set(
        'Student.Group.EducationPlan.Direction.Name',
        student.group.plan.direction.name
      )
      .set(
        'Student.Group.EducationPlan.Direction.Type',
        student.group.plan.direction.type
      );
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<Student[]> {
    const params = this._httpParams;
    return httpClient.get<Student[]>(this._apiUri, { params });
  }

  public addPages(page: number, pageSize: number): void {}
}
