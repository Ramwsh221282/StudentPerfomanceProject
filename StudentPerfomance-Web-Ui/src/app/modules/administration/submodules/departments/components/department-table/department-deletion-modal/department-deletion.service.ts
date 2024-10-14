import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../../../shared/models/api/api-constants';
import { Department } from '../../../models/departments.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class DepartmentDeletionService {
  private readonly _apiUri: string;
  private readonly _httpClient: HttpClient;

  public constructor() {
    this._apiUri = `${BASE_API_URI}/teacher-departments/api/management/remove`;
    this._httpClient = inject(HttpClient);
  }

  public remove(department: Department): Observable<Department> {
    const body = this.buildRequestBody(department);
    return this._httpClient.delete<Department>(this._apiUri, { body });
  }

  private buildRequestBody(department: Department): object {
    return {
      department: { ...department },
    };
  }
}
