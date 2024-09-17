import { Injectable } from '@angular/core';
import { StudentBaseService } from './student-base.service';
import { StudentGroup } from '../../student-groups/services/studentsGroup.interface';
import { Student } from '../models/student.interface';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class UpdateStudentService extends StudentBaseService {
  constructor() {
    super();
  }

  public update(factory: IRequestBodyFactory) {
    const body = factory.Body;
    return this.httpClient.put<Student>(`${this.baseApiUri}`, body);
  }

  public createRequestBodyFactory(
    oldData: Student,
    newData: Student,
    group: StudentGroup
  ) {
    return new HttpRequestBody(oldData, newData, group);
  }
}

class HttpRequestBody implements IRequestBodyFactory {
  private readonly _body: object;

  public constructor(oldData: Student, newData: Student, group: StudentGroup) {
    this._body = {
      oldData: {
        name: oldData.name,
        surname: oldData.surname,
        thirdname: oldData.thirdname,
        state: oldData.state,
        recordbook: oldData.recordBook,
      },
      newData: {
        name: newData.name,
        surname: newData.surname,
        thirdname: newData.thirdname,
        state: newData.state,
        recordbook: newData.recordBook,
      },
      group: {
        name: group.groupName,
      },
    };
  }

  public get Body(): object {
    return this._body;
  }
}
