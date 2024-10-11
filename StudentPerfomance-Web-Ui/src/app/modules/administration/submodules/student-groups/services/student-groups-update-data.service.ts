import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { StudentGroup } from './studentsGroup.interface';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupsUpdateDataService extends StudentGroupsService {
  public constructor() {
    super();
  }

  public update(body: IRequestBodyFactory) {
    return this.httpClient.put<StudentGroup>(
      `${this.managementApiUri}update`,
      body.Body
    );
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
      initial: {
        name: oldGroup.name,
        educationPlan: {
          year: oldGroup.plan.year == undefined ? 0 : oldGroup.plan.year,
          direction: {
            code:
              oldGroup.plan.direction.code == undefined
                ? ' '
                : oldGroup.plan.direction.code,
            name:
              oldGroup.plan.direction.name == undefined
                ? ' '
                : oldGroup.plan.direction.name,
            type:
              oldGroup.plan.direction.type == undefined
                ? ' '
                : oldGroup.plan.direction.type,
          },
        },
      },
      updated: {
        name: newGroup.name,
        educationPlan: {
          year: newGroup.plan.year == undefined ? 0 : newGroup.plan.year,
          direction: {
            code:
              newGroup.plan.direction.code == undefined
                ? ' '
                : newGroup.plan.direction.code,
            name:
              newGroup.plan.direction.name == undefined
                ? ' '
                : newGroup.plan.direction.name,
            type:
              newGroup.plan.direction.type == undefined
                ? ' '
                : newGroup.plan.direction.type,
          },
        },
      },
    };
  }

  public get Body(): object {
    return this._body;
  }
}
