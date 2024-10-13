import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupSearchService extends StudentGroupsService {
  public constructor() {
    super();
  }

  public getAllGroups(): Observable<StudentGroup[]> {
    return this.httpClient.get<StudentGroup[]>(`${this.readApiUri}all`);
  }

  public searchGroups(group: StudentGroup): Observable<StudentGroup[]> {
    const params = this.buildRequestParams(group);
    return this.httpClient.get<StudentGroup[]>(`${this.readApiUri}search`, {
      params,
    });
  }

  private buildRequestParams(group: StudentGroup): HttpParams {
    const params: HttpParams = new HttpParams()
      .set('Group.Name', group.name)
      .set('Group.EducationPlan.Year', group.plan.year)
      .set('Group.EducationPlan.Direction.Code', group.plan.direction.code)
      .set('Group.EducationPlan.Direction.Name', group.plan.direction.name)
      .set('Group.EducationPlan.Direction.Type', group.plan.direction.type);
    return params;
  }
}
