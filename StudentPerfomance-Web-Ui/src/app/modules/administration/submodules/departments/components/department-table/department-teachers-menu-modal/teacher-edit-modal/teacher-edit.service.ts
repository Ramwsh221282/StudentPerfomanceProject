import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../../shared/models/api/api-constants';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class TeacherEditService {
  private readonly _apiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor() {
    this._apiUri = `${BASE_API_URI}/teacher/api/management/update`;
    this._httpClient = inject(HttpClient);
  }

  public update(initial: Teacher, updated: Teacher): Observable<Teacher> {
    const body = this.buildRequestBody(initial, updated);
    return this._httpClient.put<Teacher>(this._apiUri, body);
  }

  private buildRequestBody(initial: Teacher, updated: Teacher): object {
    return {
      initial: { ...initial },
      updated: { ...updated },
    };
  }
}
