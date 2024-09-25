import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';

@Injectable({
  providedIn: 'any',
})
export class BaseService {
  protected readonly httpClient: HttpClient = inject(HttpClient);
  protected readonly baseApiUri: string = `${BASE_API_URI}/EducationDirections`;
}
