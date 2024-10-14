import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class TeacherRemoveService {
  private readonly _apiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor() {
    this._httpClient = inject(HttpClient);
    this._apiUri = `${BASE_API_URI}/teacher/api/management/remove`;
  }

  public remove(teacher: Teacher): Observable<Teacher> {
    const body = this.buildRequestBody(teacher);
    return this._httpClient.delete<Teacher>(this._apiUri, { body });
  }

  private buildRequestBody(teacher: Teacher): object {
    return {
      teacher: { ...teacher },
    };
  }
}
