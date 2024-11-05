import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../../../../../users/services/auth.service';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { TeacherJournalStudent } from '../../../../models/teacher-journal-students';
import { TokenPayloadBuilder } from '../../../../../../shared/models/common/token-contract/token-payload-builder';
import { TeacherJournalDiscipline } from '../../../../models/teacher-journal-disciplines';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class TeacherAssignmentsService {
  private readonly apiUri: string;

  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService
  ) {
    this.apiUri = `${BASE_API_URI}/app/assignment-sessions/make-assignment`;
  }

  public makeAssignment(
    student: TeacherJournalStudent,
    discipline: TeacherJournalDiscipline
  ): Observable<any> {
    const paylod = this.buildPayload(student, discipline);
    return this._httpClient.post<any>(this.apiUri, paylod);
  }

  private buildPayload(
    student: TeacherJournalStudent,
    discipline: TeacherJournalDiscipline
  ): object {
    return {
      token: TokenPayloadBuilder(this._authService.userData),
      assignment: {
        id: student.id,
        mark: student.assignment.value,
      },
    };
  }
}
