import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { Department } from '../../models/departments.interface';
import { inject, Injectable } from '@angular/core';
import { DepartmentDefaultFetchPolicy } from '../../models/fetch-policies/default-fetch-policy';
import { Observable } from 'rxjs';
//import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { IObservableFetchable } from '../../../../../../shared/models/fetch-policices/iobservable-fetchable.interface';
import { AuthService } from '../../../../../../pages/user-page/services/auth.service';
import { AppConfigService } from '../../../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class DepartmentDataService
  implements IObservableFetchable<Department[]>
{
  private readonly _authService: AuthService;
  private readonly _httpClient: HttpClient;
  private _currentPolicy: IFetchPolicy<Department[]>;

  public constructor(private readonly _appConfig: AppConfigService) {
    this._httpClient = inject(HttpClient);
    this._authService = inject(AuthService);
    this._currentPolicy = new DepartmentDefaultFetchPolicy(
      this._authService,
      this._appConfig,
    );
  }

  public setPolicy(policy: IFetchPolicy<Department[]>): void {
    this._currentPolicy = policy;
  }

  public fetch(): Observable<Department[]> {
    return this._currentPolicy.executeFetchPolicy(this._httpClient);
  }

  public addPages(page: number, pageSize: number): void {
    this._currentPolicy.addPages(page, pageSize);
  }

  public getAllDepartments(): Observable<Department[]> {
    //const apiUri: string = `${BASE_API_URI}/api/teacher-departments/all`;
    const apiUri: string = `${this._appConfig.baseApiUri}/api/teacher-departments/all`;
    const headers = this.buildHttpHeaders();
    return this._httpClient.get<Department[]>(apiUri, { headers: headers });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
