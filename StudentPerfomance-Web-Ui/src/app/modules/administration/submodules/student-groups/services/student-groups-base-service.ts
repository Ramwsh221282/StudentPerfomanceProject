import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';

export class StudentGroupsService {
  protected managementApiUri: string = `${BASE_API_URI}/student-groups/api/management/`;
  protected readApiUri: string = `${BASE_API_URI}/student-groups/api/read/`;
  protected httpClient: HttpClient;

  public constructor() {
    this.httpClient = inject(HttpClient);
  }
}
