import { Injectable } from '@angular/core';
import { BaseSemesterService } from './base-semester.service';
import { Observable } from 'rxjs';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';
import { Semester } from '../models/semester.interface';

@Injectable({
  providedIn: 'any',
})
export class SemesterDeleteService extends BaseSemesterService {
  public constructor() {
    super();
  }

  public delete(factory: IRequestBodyFactory): Observable<Semester> {
    const body = factory.Body;
    return this.httpClient.delete<Semester>(`${this.baseApiUri}`, { body });
  }

  public createRequestBodyFactory(semester: Semester): IRequestBodyFactory {
    return new HttpRequestBody(semester);
  }
}

class HttpRequestBody implements IRequestBodyFactory {
  private readonly _body: object;

  public constructor(semester: Semester) {
    this._body = {
      semester: {
        number: semester.number,
      },
      group: {
        name: semester.groupName,
      },
    };
  }

  public get Body(): object {
    return this._body;
  }
}
