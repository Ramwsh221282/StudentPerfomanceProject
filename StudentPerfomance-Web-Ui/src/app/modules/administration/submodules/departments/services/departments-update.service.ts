import { Injectable } from '@angular/core';
import { DepartmentsBaseService } from './departments-base.service';
import { Observable } from 'rxjs';
import { Department } from '../models/departments.interface';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class DepartmentsUpdateService extends DepartmentsBaseService {
  constructor() {
    super();
  }

  public update(factory: IRequestBodyFactory): Observable<Department> {
    const body = factory.Body;
    return this.httpClient.put<Department>(
      `${this.managementApiUri}update`,
      body
    );
  }

  public createRequestBodyFactory(
    oldData: Department,
    newData: Department
  ): IRequestBodyFactory {
    return new HttpRequestBodyFactory(oldData, newData);
  }
}

class HttpRequestBodyFactory implements IRequestBodyFactory {
  private readonly _body: object;

  public constructor(oldData: Department, newData: Department) {
    this._body = {
      oldDepartment: {
        name: oldData.name,
      },
      newDepartment: {
        name: newData.name,
      },
    };
  }

  public get Body(): object {
    return this._body;
  }
}
