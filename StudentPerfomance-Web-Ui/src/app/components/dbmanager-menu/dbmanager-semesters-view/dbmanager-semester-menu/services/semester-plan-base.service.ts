import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';

export abstract class SemesterPlanBaseService {
  protected httpClient: HttpClient;
  protected baseApiUri: string;

  public constructor() {
    this.httpClient = inject(HttpClient);
    this.baseApiUri = 'http://localhost:5005/SemesterPlans';
  }
}
