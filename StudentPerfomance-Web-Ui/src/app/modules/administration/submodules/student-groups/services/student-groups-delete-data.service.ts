import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupsDeleteDataService extends StudentGroupsService {
  public constructor() {
    super();
  }

  public delete(factory: IRequestBodyFactory) {
    const body = factory.Body;
    return this.httpClient.delete<StudentGroup>(
      `${this.managementApiUri}remove`,
      { body }
    );
  }

  public createRequestBodyFactory(group: StudentGroup): IRequestBodyFactory {
    return new HttpRequestBody(group);
  }
}

class HttpRequestBody implements IRequestBodyFactory {
  private readonly _body: object;

  public constructor(group: StudentGroup) {
    this._body = { group: { name: group.name } };
  }

  public get Body(): object {
    return this._body;
  }
}
