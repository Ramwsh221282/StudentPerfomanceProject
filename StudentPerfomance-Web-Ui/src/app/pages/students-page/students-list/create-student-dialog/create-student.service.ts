import { Injectable } from '@angular/core';
import { BaseHttpService } from '../../../../shared/models/common/base-http/base-http.service';
import { Student } from '../../../../modules/administration/submodules/students/models/student.interface';
import { StudentPayloadBuilder } from '../../../../modules/administration/submodules/students/models/contracts/student-contracts/student-payload-builder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class CreateStudentService extends BaseHttpService {
  public create(student: Student): Observable<Student> {
    const url = `${this._config.baseApiUri}/api/students`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(student);
    return this._http.post<Student>(url, payload, { headers: headers });
  }

  private buildPayload(student: Student): object {
    return {
      student: StudentPayloadBuilder(student),
    };
  }
}
