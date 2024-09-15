import { Injectable } from '@angular/core';
import { DepartmentsBaseService } from './departments-base.service';
import { Department } from './departments.interface';
import { IRequestBodyFactory } from '../../../../models/RequestParamsFactory/irequest-body-factory.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DepartmentsDeleteService extends DepartmentsBaseService {
  constructor() {
    super();
  }

  public delete(factory: IRequestBodyFactory): Observable<Department> {
    const body = factory.Body;
    return this.httpClient.delete<Department>(`${this.baseApiUri}`, { body });
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
