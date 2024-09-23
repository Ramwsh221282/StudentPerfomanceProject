import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';

export abstract class SemesterPlanBaseService {
  protected httpClient: HttpClient;
  protected baseApiUri: string;

  public constructor() {
    this.httpClient = inject(HttpClient);
    this.baseApiUri = `${BASE_API_URI}/SemesterPlans`;
  }
}
