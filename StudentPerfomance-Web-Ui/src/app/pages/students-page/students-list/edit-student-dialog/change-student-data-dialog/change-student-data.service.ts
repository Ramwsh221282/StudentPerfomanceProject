import { Injectable } from '@angular/core';
import { BaseHttpService } from '../../../../../shared/models/common/base-http/base-http.service';
import { StudentPayloadBuilder } from '../../../../../modules/administration/submodules/students/models/contracts/student-contracts/student-payload-builder';
import { Student } from '../../../../../modules/administration/submodules/students/models/student.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class ChangeStudentDataService extends BaseHttpService {
  public edit(initial: Student, updated: Student): Observable<Student> {
    const url = `${this._config.baseApiUri}/api/students`;
    const payload = this.buildPayload(initial, updated);
    const headers = this.buildHttpHeaders();
    return this._http.put<Student>(url, payload, { headers: headers });
  }

  public buildPayload(initial: Student, updated: Student): object {
    return {
      student: StudentPayloadBuilder(initial),
      updated: StudentPayloadBuilder(updated),
    };
  }
}
