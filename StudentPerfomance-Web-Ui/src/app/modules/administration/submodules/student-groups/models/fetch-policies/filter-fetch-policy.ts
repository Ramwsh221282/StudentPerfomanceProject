import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';

export class FilterFetchPolicy implements IFetchPolicy<StudentGroup[]> {
  private readonly _apiUri: string = `${BASE_API_URI}/student-groups/api/read/filter`;
  private readonly _group: StudentGroup;
  private _httpParams: HttpParams;

  public constructor(group: StudentGroup) {
    this._group = group;
    this._httpParams = new HttpParams()
      .set('Group.Name', group.groupName)
      .set('Group.EducationPlan.Year', group.plan.year)
      .set('Group.EducationPlan.Direction.Code', group.plan.direction.code)
      .set('Group.EducationPlan.Direction.Name', group.plan.direction.name)
      .set('Group.EducationPlan.Direction.Type', group.plan.direction.type);
  }

  public executeFetchPolicy(
    httpClient: HttpClient
  ): Observable<StudentGroup[]> {
    const params = this._httpParams;
    return httpClient.get<StudentGroup[]>(this._apiUri, { params });
  }

  public addPages(page: number, pageSize: number): void {
    this._httpParams = new HttpParams()
      .set('Group.Name', this._group.groupName)
      .set('Group.EducationPlan.Year', this._group.plan.year)
      .set(
        'Group.EducationPlan.Direction.Code',
        this._group.plan.direction.code
      )
      .set(
        'Group.EducationPlan.Direction.Name',
        this._group.plan.direction.name
      )
      .set(
        'Group.EducationPlan.Direction.Type',
        this._group.plan.direction.type
      )
      .set('Page', page)
      .set('PageSize', pageSize);
  }
}
