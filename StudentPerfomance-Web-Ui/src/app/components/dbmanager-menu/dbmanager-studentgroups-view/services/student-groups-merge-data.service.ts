import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { IRequestBodyFactory } from '../../../../models/RequestParamsFactory/irequest-body-factory.interface';

@Injectable({
  providedIn: 'root',
})
export class StudentGroupsMergeDataService extends StudentGroupsService {
  public constructor() {
    super();
  }

  public merge(factory: IRequestBodyFactory) {
    const body = factory.Body;
    return this.httpClient.put<StudentGroup>(
      `${this.baseApiUri}/mergeGroups`,
      body
    );
  }

  public createRequestBodyFactory(
    targetGroup: StudentGroup,
    mergeGroup: StudentGroup
  ): IRequestBodyFactory {
    return new HttpRequestBody(targetGroup, mergeGroup);
  }
}

class HttpRequestBody implements IRequestBodyFactory {
  private readonly _body: object;

  public constructor(targetGroup: StudentGroup, mergeGroup: StudentGroup) {
    this._body = {
      targetGroup: {
        name: targetGroup.groupName,
      },
      mergeGroup: {
        name: mergeGroup.groupName,
      },
    };
  }

  public get Body(): object {
    return this._body;
  }
}
