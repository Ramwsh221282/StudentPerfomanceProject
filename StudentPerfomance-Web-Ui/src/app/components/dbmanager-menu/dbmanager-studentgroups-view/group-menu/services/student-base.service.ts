import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';

export class StudentBaseService {
  protected readonly baseApiUri: string;
  protected readonly httpClient: HttpClient;

  public constructor() {
    this.baseApiUri = 'http://localhost:5005/Students';
    this.httpClient = inject(HttpClient);
  }
}
