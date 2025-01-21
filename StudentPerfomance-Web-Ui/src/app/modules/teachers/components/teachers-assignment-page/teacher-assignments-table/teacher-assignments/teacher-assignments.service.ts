import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../../../../../../pages/user-page/services/auth.service';
import { TeacherJournalStudent } from '../../../../models/teacher-journal-students';
import { TokenPayloadBuilder } from '../../../../../../shared/models/common/token-contract/token-payload-builder';
import { TeacherJournalDiscipline } from '../../../../models/teacher-journal-disciplines';
import { Observable } from 'rxjs';
import { AppConfigService } from '../../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class TeacherAssignmentsService {
  private readonly apiUri: string;

  public constructor(
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService,
    private readonly _configService: AppConfigService,
  ) {
    //this.apiUri = `${BASE_API_URI}/app/assignment-sessions/make-assignment`;
    this.apiUri = `${this._configService.baseApiUri}/app/assignment-sessions/make-assignment`;
  }

  public makeAssignment(
    student: TeacherJournalStudent,
    discipline: TeacherJournalDiscipline,
  ): Observable<any> {
    const paylod = this.buildPayload(student);
    const headers = this.buildHttpHeaders();
    return this._httpClient.post<any>(this.apiUri, paylod, {
      headers: headers,
    });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }

  private buildPayload(student: TeacherJournalStudent): object {
    return {
      token: TokenPayloadBuilder(this._authService.userData),
      assignment: {
        id: student.id,
        mark: student.assignment.value,
      },
    };
  }
}
