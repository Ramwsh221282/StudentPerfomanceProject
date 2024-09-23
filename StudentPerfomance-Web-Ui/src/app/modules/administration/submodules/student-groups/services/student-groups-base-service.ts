import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';

export class StudentGroupsService {
  protected baseApiUri: string;
  protected httpClient: HttpClient;

  public constructor() {
    this.baseApiUri = `${BASE_API_URI}/StudentGroups`;
    this.httpClient = inject(HttpClient);
  }
}
