import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { StudentGroupsModule } from '../student-groups.module';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupsUpdateDataService extends StudentGroupsService {
  public constructor() {
    super();
  }

  public update(body: IRequestBodyFactory) {
    return this.httpClient.put<StudentGroup>(this.baseApiUri, body.Body);
  }

  public requestBodyFactory(
    oldGroup: StudentGroup,
    newGroup: StudentGroup
  ): IRequestBodyFactory {
    return new HttpRequestBody(oldGroup, newGroup);
  }
}

class HttpRequestBody implements IRequestBodyFactory {
  private readonly _body: object;

  public constructor(oldGroup: StudentGroup, newGroup: StudentGroup) {
    this._body = {
      oldGroup: { name: oldGroup.groupName },
      newGroup: { name: newGroup.groupName },
    };
  }

  public get Body(): object {
    return this._body;
  }
}
