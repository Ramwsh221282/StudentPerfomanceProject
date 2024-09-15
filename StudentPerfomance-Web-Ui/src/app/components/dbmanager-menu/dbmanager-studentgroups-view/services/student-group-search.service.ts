import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { IRequestParamsFactory } from '../../../../models/RequestParamsFactory/irequest-params-factory.interface';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class StudentGroupSearchService extends StudentGroupsService {
  public constructor() {
    super();
  }

  public searchByName(factory: IRequestParamsFactory) {
    const params = factory.Params;
    return this.httpClient.get<StudentGroup[]>(
      `${this.baseApiUri}/bySearchNameParam`,
      {
        params,
      }
    );
  }

  public createRequestParamsFactory(
    group: StudentGroup
  ): IRequestParamsFactory {
    return new HttpRequestParams(group);
  }
}

class HttpRequestParams implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(group: StudentGroup) {
    this._httpParams = new HttpParams().set('Group.Name', group.groupName);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}
