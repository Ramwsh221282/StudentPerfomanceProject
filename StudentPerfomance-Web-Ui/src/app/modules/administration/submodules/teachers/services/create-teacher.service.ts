import { Injectable } from '@angular/core';
import { BaseTeacherService } from './base-teacher.service';
import { Department } from '../../departments/models/departments.interface';
import { Teacher } from '../models/teacher.interface';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class CreateTeacherService extends BaseTeacherService {
  public constructor() {
    super();
  }

  public create(factory: IRequestBodyFactory) {
    const body = factory.Body;
    return this.httpClient.post<Teacher>(`${this.baseApiUri}`, body);
  }

  public createRequestBodyFactory(
    teacher: Teacher,
    department: Department
  ): IRequestBodyFactory {
    return new HttpRequestBody(teacher, department);
  }
}

class HttpRequestBody implements IRequestBodyFactory {
  private readonly _body: object;

  public constructor(teacher: Teacher, department: Department) {
    this._body = {
      teacher: {
        name: teacher.name,
        surname: teacher.surname,
        thirdname: teacher.thirdname,
      },
      department: {
        name: department.name,
      },
    };
  }

  public get Body(): object {
    return this._body;
  }
}
