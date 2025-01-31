import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../../../../../shared/models/common/base-http/base-http.service';
import { TeacherJournalStudent } from '../../../models/teacher-journal-students';
import { TeacherJournalDiscipline } from '../../../models/teacher-journal-disciplines';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class TeacherAssignmentsService extends BaseHttpService {
  public makeAssignment(
    student: TeacherJournalStudent,
    discipline: TeacherJournalDiscipline,
  ): Observable<any> {
    const url = `${this._config.baseApiUri}/app/assignment-sessions/make-assignment`;
    const payload = this.buildPayload(student);
    const headers = this.buildHttpHeaders();
    return this._http.post<any>(url, payload, {
      headers: headers,
    });
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
