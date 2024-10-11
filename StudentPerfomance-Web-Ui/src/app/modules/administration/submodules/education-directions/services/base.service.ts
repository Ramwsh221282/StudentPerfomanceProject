import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';

@Injectable({
  providedIn: 'any',
})
export class BaseService {
  protected readonly httpClient: HttpClient;
  protected readonly managementApiUri: string;
  protected readonly readApiUri: string;

  public constructor() {
    this.httpClient = inject(HttpClient);
    this.managementApiUri = `${BASE_API_URI}/education-directions/api/management/`;
    this.readApiUri = `${BASE_API_URI}/education-directions/api/read/`;
  }
}
