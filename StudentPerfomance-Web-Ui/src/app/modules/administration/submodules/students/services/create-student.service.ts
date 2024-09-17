import { Injectable } from '@angular/core';
import { StudentBaseService } from './student-base.service';
import { Student } from '../models/student.interface';
import { StudentsModule } from '../students.module';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class CreateStudentService extends StudentBaseService {
  constructor() {
    super();
  }

  public create(factory: IRequestBodyFactory) {
    const body = factory.Body;
    return this.httpClient.post<Student>(`${this.baseApiUri}`, body);
  }

  public createRequestBodyFactory(student: Student): IRequestBodyFactory {
    return new HttpRequestBody(student);
  }
}

class HttpRequestBody implements IRequestBodyFactory {
  private readonly _body: object;

  public constructor(student: Student) {
    this._body = {
      student: {
        name: student.name,
        surname: student.surname,
        thirdname: student.thirdname,
        state: student.state,
        recordbook: student.recordBook,
      },
      group: {
        name: student.groupName,
      },
    };
  }

  public get Body(): object {
    return this._body;
  }
}
