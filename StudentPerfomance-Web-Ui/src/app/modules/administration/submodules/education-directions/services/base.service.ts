import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AppConfigService } from '../../../../../app.config.service';

//import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';

@Injectable({
  providedIn: 'any',
})
export class BaseService {
  protected readonly httpClient: HttpClient;
  protected readonly managementApiUri: string;
  protected readonly readApiUri: string;

  public constructor() {
    this.httpClient = inject(HttpClient);
    const appConfig = inject(AppConfigService);
    // this.managementApiUri = `${BASE_API_URI}/education-directions/api/management/`;
    // this.readApiUri = `${BASE_API_URI}/education-directions/api/read/`;
    this.managementApiUri = `${appConfig.baseApiUri}/education-directions/api/management/`;
    this.readApiUri = `${appConfig.baseApiUri}/education-directions/api/read/`;
  }
}
