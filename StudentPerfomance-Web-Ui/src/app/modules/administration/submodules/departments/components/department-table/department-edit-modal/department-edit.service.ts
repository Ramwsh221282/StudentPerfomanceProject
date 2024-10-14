import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { Department } from '../../../models/departments.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class DepartmentEditService {
  private readonly _apiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor() {
    this._apiUri = `${BASE_API_URI}/teacher-departments/api/management/update`;
    this._httpClient = inject(HttpClient);
  }

  public update(
    initial: Department,
    updated: Department
  ): Observable<Department> {
    const body = this.buildRequestBody(initial, updated);
    console.log(body);
    return this._httpClient.put<Department>(this._apiUri, body);
  }

  private buildRequestBody(initial: Department, updated: Department): object {
    return {
      initial: { ...initial },
      updated: { ...updated },
    };
  }
}
