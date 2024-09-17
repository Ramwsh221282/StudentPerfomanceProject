import { Injectable } from '@angular/core';
import { StudentBaseService } from './student-base.service';
import { StudentsModule } from '../students.module';
import { Student } from '../models/student.interface';
import { IRequestBodyFactory } from '../../../../../models/RequestParamsFactory/irequest-body-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class DeleteStudentService extends StudentBaseService {
  constructor() {
    super();
  }

  public delete(factory: IRequestBodyFactory) {
    const body = factory.Body;
    return this.httpClient.delete<Student>(`${this.baseApiUri}`, { body });
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
    };
  }

  public get Body(): object {
    return this._body;
  }
}
