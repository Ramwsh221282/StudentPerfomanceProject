import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';

export class BaseService {
  protected readonly httpClient: HttpClient = inject(HttpClient);
  protected readonly managementApiUri: string = `${BASE_API_URI}/education-plans/api/management/`;
  protected readonly readApiUri: string = `${BASE_API_URI}/education-plans/api/read/`;
}
