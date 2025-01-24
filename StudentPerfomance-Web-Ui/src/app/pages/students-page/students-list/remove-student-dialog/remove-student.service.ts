import { BaseHttpService } from '../../../../shared/models/common/base-http/base-http.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../../../../modules/administration/submodules/students/models/student.interface';

@Injectable({
  providedIn: 'any',
})
export class RemoveStudentService extends BaseHttpService {
  public remove(student: Student): Observable<Student> {
    const url = `${this._config.baseApiUri}/api/students`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(student);
    return this._http.delete<Student>(url, { headers: headers, body: payload });
  }

  private buildPayload(student: Student): object {
    return {
      student: {
        name: student.name,
        surname: student.surname,
        patronymic: student.patronymic,
        state: student.state,
        recordbook: student.recordbook,
        groupName: student.group.name,
      },
    };
  }
}
