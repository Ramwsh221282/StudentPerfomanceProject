import { Injectable } from '@angular/core';
import { BaseTeacherService } from './base-teacher.service';
import { Teacher } from '../models/teacher.interface';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class UpdateTeacherService extends BaseTeacherService {
  constructor() {
    super();
  }

  public update(factory: IRequestBodyFactory) {
    const body = factory.Body;
    return this.httpClient.put<Teacher>(`${this.baseApiUri}`, body);
  }

  public createRequestBodyFactory(
    oldData: Teacher,
    newData: Teacher
  ): IRequestBodyFactory {
    return new RequestBodyParams(oldData, newData);
  }
}

class RequestBodyParams implements IRequestBodyFactory {
  private readonly _body: object;

  public constructor(oldData: Teacher, newData: Teacher) {
    this._body = {
      oldTeacher: {
        name: oldData.name,
        surname: oldData.surname,
        thirdname: oldData.thirdname,
      },
      newTeacher: {
        name: newData.name,
        surname: newData.surname,
        thirdname: newData.thirdname,
      },
    };
  }

  public get Body(): object {
    return this._body;
  }
}
