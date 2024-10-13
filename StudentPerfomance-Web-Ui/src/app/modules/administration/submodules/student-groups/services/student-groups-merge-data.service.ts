import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupsMergeDataService extends StudentGroupsService {
  public constructor() {
    super();
  }

  public merge(factory: IRequestBodyFactory) {
    const body = factory.Body;
    return this.httpClient.put<StudentGroup>(
      `${this.managementApiUri}group-merge`,
      body
    );
  }

  public createRequestBodyFactory(
    initial: StudentGroup,
    target: StudentGroup
  ): IRequestBodyFactory {
    return new HttpRequestBody(initial, target);
  }
}

class HttpRequestBody implements IRequestBodyFactory {
  private readonly _body: object;

  public constructor(initial: StudentGroup, target: StudentGroup) {
    this._body = {
      initial: { ...initial },
      target: { ...target },
    };
  }

  public get Body(): object {
    return this._body;
  }
}
