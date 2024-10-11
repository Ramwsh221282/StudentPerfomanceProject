import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { HttpParams } from '@angular/common/http';
import { IRequestParamsFactory } from '../../../../../models/RequestParamsFactory/irequest-params-factory.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupSearchService extends StudentGroupsService {
  public constructor() {
    super();
  }

  public searchByName(
    factory: IRequestParamsFactory
  ): Observable<StudentGroup[]> {
    const params = factory.Params;
    return this.httpClient.get<StudentGroup[]>(`${this.readApiUri}/search`, {
      params,
    });
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
