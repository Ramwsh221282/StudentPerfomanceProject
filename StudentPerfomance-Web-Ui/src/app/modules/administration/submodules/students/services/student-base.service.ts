import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';

export class StudentBaseService {
  protected readonly baseApiUri: string;
  protected readonly httpClient: HttpClient;

  public constructor() {
    this.baseApiUri = `${BASE_API_URI}/Students`;
    this.httpClient = inject(HttpClient);
  }
}
