import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';

export class BaseTeacherService {
  protected readonly httpClient: HttpClient;
  protected readonly baseApiUri: string;

  public constructor() {
    this.httpClient = inject(HttpClient);
    this.baseApiUri = `${BASE_API_URI}/Teachers`;
  }
}
