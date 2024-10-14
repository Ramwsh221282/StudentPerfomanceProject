import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class TeacherCreationService {
  private readonly _httpClient: HttpClient;
  private readonly _apiUri: string;

  public constructor() {
    this._httpClient = inject(HttpClient);
    this._apiUri = `${BASE_API_URI}/teacher/api/management/create`;
  }

  public create(teacher: Teacher): Observable<Teacher> {
    const body = this.buildRequestBody(teacher);
    return this._httpClient.post<Teacher>(this._apiUri, body);
  }

  private buildRequestBody(teacher: Teacher): object {
    return {
      teacher: { ...teacher },
    };
  }
}
