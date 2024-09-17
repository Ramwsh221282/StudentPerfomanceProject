import { Injectable } from '@angular/core';
import { DepartmentsBaseService } from './departments-base.service';
import { DepartmentsModule } from '../departments.module';
import { Department } from '../models/departments.interface';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class DepartmentsCreateService extends DepartmentsBaseService {
  public constructor() {
    super();
  }

  public create(factory: IRequestBodyFactory) {
    const body = factory.Body;
    return this.httpClient.post<Department>(`${this.baseApiUri}`, body);
  }

  public createRequestBodyFactory(department: Department) {
    return new HttpRequestBody(department);
  }
}

class HttpRequestBody implements IRequestBodyFactory {
  private readonly _body: object;

  public constructor(department: Department) {
    this._body = {
      department: {
        name: department.name,
      },
    };
  }

  public get Body(): object {
    return this._body;
  }
}
