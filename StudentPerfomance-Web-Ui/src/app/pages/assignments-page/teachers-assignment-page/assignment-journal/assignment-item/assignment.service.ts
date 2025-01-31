import { BaseHttpService } from '../../../../../shared/models/common/base-http/base-http.service';
import { Injectable } from '@angular/core';
import { TeacherJournalStudent } from '../../../models/teacher-journal-students';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'any' })
export class AssignmentService extends BaseHttpService {
  public makeAssignment(student: TeacherJournalStudent): Observable<any> {
    const payload = this.buildPayload(student);
    const headers = this.buildHttpHeaders();
    const url = `${this._config.baseApiUri}/app/assignment-sessions/make-assignment`;
    return this._http.post<any>(url, payload, { headers: headers });
  }

  private buildPayload(student: TeacherJournalStudent): object {
    return {
      token: TokenPayloadBuilder(this._auth.userData),
      assignment: {
        id: student.id,
        mark: student.assignment.value,
      },
    };
  }
}
