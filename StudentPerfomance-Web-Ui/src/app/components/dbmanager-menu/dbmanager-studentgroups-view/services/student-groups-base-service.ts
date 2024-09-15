import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';

export class StudentGroupsService {
  protected baseApiUri: string;
  protected httpClient: HttpClient;

  public constructor() {
    this.baseApiUri = 'http://localhost:5005/StudentGroups';
    this.httpClient = inject(HttpClient);
  }
}
