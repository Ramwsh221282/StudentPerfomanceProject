import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';

export class BaseSemesterService {
  protected readonly httpClient: HttpClient;
  protected readonly baseApiUri: string;

  public constructor() {
    this.httpClient = inject(HttpClient);
    this.baseApiUri = 'http://localhost:5005/Semesters';
  }
}
