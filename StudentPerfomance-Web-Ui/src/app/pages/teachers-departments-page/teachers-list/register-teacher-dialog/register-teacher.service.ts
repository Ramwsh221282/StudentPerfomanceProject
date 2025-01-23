import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../../user-page/services/auth.service';
import { AppConfigService } from '../../../../app.config.service';
import { Teacher } from '../../../../modules/administration/submodules/teachers/models/teacher.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class RegisterTeacherService {
  public constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _config: AppConfigService,
  ) {}

  public register(teacher: Teacher, email: string): Observable<Teacher> {
    const url = `${this._config.baseApiUri}/api/teachers/asuser`;
    const headers = this.buildHttpHeaders();
    const payload = this.buildPayload(teacher, email);
    return this._http.post<Teacher>(url, payload, { headers: headers });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
  }

  private buildPayload(teacher: Teacher, email: string): object {
    return {
      name: teacher.name,
      surname: teacher.surname,
      patronymic: teacher.patronymic,
      email: email,
      teacherId: teacher.id,
    };
  }
}
