import { Injectable } from '@angular/core';
import { BaseTeacherService } from './base-teacher.service';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Department } from '../../departments/models/departments.interface';
import { Teacher } from '../models/teacher.interface';
import { IRequestParamsFactory } from '../../../../../models/RequestParamsFactory/irequest-params-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class SearchTeacherService extends BaseTeacherService {
  public constructor() {
    super();
  }

  public search(factory: IRequestParamsFactory): Observable<Teacher[]> {
    const params = factory.Params;
    return this.httpClient.get<Teacher[]>(`${this.baseApiUri}/byDepartment`, {
      params,
    });
  }

  public createRequestParamFactory(
    department: Department
  ): IRequestParamsFactory {
    return new HttpRequestParam(department);
  }
}
class HttpRequestParam implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(department: Department) {
    this._httpParams = new HttpParams().set('Department.Name', department.name);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}
