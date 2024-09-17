import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { HttpParams } from '@angular/common/http';
import { StudentGroupsModule } from '../student-groups.module';
import { IRequestParamsFactory } from '../../../../../models/RequestParamsFactory/irequest-params-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupsFetchDataService extends StudentGroupsService {
  private _studentGroups: StudentGroup[] = [];

  public constructor() {
    super();
  }

  public get studentGroups(): StudentGroup[] {
    return this._studentGroups;
  }

  public fetch(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<StudentGroup[]>(`${this.baseApiUri}/byPage`, {
        params,
      })
      .subscribe((response) => {
        this._studentGroups = response;
      });
  }

  public filter(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<StudentGroup[]>(`${this.baseApiUri}/byFilter`, { params })
      .subscribe((response) => {
        this._studentGroups = response;
      });
  }

  public createFilterRequestFactory(
    group: StudentGroup,
    page: number,
    pageSize: number
  ): IRequestParamsFactory {
    return new HttpRequestParams(group, page, pageSize);
  }
}

class HttpRequestParams implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(group: StudentGroup, page: number, pageSize: number) {
    this._httpParams = new HttpParams()
      .set('Group.Name', group.groupName)
      .set('Page', page)
      .set('PageSize', pageSize);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}
