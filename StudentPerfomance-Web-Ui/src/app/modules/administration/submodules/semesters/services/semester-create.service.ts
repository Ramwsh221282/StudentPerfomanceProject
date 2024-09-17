import { Injectable } from '@angular/core';
import { BaseSemesterService } from './base-semester.service';
import { Observable } from 'rxjs';
import { SemestersModule } from '../semesters.module';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';
import { Semester } from '../models/semester.interface';

@Injectable({
  providedIn: 'any',
})
export class SemesterCreateService extends BaseSemesterService {
  public constructor() {
    super();
  }

  public create(factory: IRequestBodyFactory): Observable<Semester> {
    const body = factory.Body;
    return this.httpClient.post<Semester>(`${this.baseApiUri}`, body);
  }

  public createRequestBody(semester: Semester): IRequestBodyFactory {
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
