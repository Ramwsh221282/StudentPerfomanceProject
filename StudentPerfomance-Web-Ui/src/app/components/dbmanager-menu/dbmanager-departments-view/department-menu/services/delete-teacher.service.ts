import { Injectable } from '@angular/core';
import { BaseTeacherService } from './base-teacher.service';
import { Teacher } from '../models/teacher.interface';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';

@Injectable({
  providedIn: 'root',
})
export class DeleteTeacherService extends BaseTeacherService {
  public constructor() {
    super();
  }

  public delete(factory: IRequestBodyFactory) {
    const body = factory.Body;
    return this.httpClient.delete<Teacher>(`${this.baseApiUri}`, { body });
  }

  public createRequestBodyFactory(teacher: Teacher): IRequestBodyFactory {
    return new HttpRequestBody(teacher);
  }
}

class HttpRequestBody implements IRequestBodyFactory {
  private readonly _body: object;

  public constructor(teacher: Teacher) {
    this._body = {
      teacher: {
        name: teacher.name,
        surname: teacher.surname,
        thirdname: teacher.thirdname,
      },
    };
  }

  public get Body(): object {
    return this._body;
  }
}
