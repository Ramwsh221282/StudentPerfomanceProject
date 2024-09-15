import { Injectable } from '@angular/core';
import { DepartmentsBaseService } from './departments-base.service';
import { Department } from './departments.interface';
import { IRequestBodyFactory } from '../../../../models/RequestParamsFactory/irequest-body-factory.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DepartmentsUpdateService extends DepartmentsBaseService {
  constructor() {
    super();
  }

  public update(factory: IRequestBodyFactory): Observable<Department> {
    const body = factory.Body;
    return this.httpClient.put<Department>(`${this.baseApiUri}`, body);
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
