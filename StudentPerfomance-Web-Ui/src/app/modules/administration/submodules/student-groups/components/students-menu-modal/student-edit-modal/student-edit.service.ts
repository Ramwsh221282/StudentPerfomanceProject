import { HttpClient } from '@angular/common/http';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { Student } from '../../../../students/models/student.interface';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class StudentEditService {
  private readonly _apiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor() {
    this._apiUri = `${BASE_API_URI}/student/api/management/update`;
    this._httpClient = inject(HttpClient);
  }

  public update(initial: Student, updated: Student): Observable<Student> {
    const body = this.buildRequestBody(initial, updated);
    return this._httpClient.put<Student>(this._apiUri, body);
  }

  private buildRequestBody(initial: Student, updated: Student): object {
    return {
      initial: { ...initial },
      updated: { ...updated },
    };
  }
}
