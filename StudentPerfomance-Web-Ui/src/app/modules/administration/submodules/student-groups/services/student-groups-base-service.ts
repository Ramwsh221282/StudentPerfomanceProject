import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { AppConfigService } from '../../../../../app.config.service';

//import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';

export class StudentGroupsService {
  protected readonly managementApiUri: string;
  protected readonly readApiUri: string;
  protected readonly httpClient: HttpClient;

  public constructor() {
    const appConfig = inject(AppConfigService);
    this.httpClient = inject(HttpClient);
    //this.managementApiUri = `${BASE_API_URI}/student-groups/api/management/`;
    //this.readApiUri = = `${BASE_API_URI}/student-groups/api/read/`;
    this.managementApiUri = `${appConfig.baseApiUri}/student-groups/api/management/`;
    this.readApiUri = `${appConfig.baseApiUri}/student-groups/api/read/`;
  }
}
