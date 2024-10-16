import { HttpClient } from '@angular/common/http';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { IFetchable } from '../../../../../../shared/models/fetch-policices/ifetchable-interface';
import { Department } from '../../models/departments.interface';
import { inject, Injectable } from '@angular/core';
import { DepartmentDefaultFetchPolicy } from '../../models/fetch-policies/default-fetch-policy';
import { Observable } from 'rxjs';
import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';

@Injectable({
  providedIn: 'any',
})
export class DepartmentDataService implements IFetchable<Department[]> {
  private readonly _httpClient: HttpClient;
  private _currentPolicy: IFetchPolicy<Department[]>;
  private _departments: Department[];

  public constructor() {
    this._httpClient = inject(HttpClient);
    this._departments = [];
    this._currentPolicy = new DepartmentDefaultFetchPolicy();
  }

  public get departments(): Department[] {
    return this._departments;
  }

  public setPolicy(policy: IFetchPolicy<Department[]>): void {
    this._currentPolicy = policy;
  }

  public fetch(): void {
    this._currentPolicy
      .executeFetchPolicy(this._httpClient)
      .subscribe((response) => {
        this._departments = response;
      });
  }

  public addPages(page: number, pageSize: number): void {
    this._currentPolicy.addPages(page, pageSize);
  }

  public getAllDepartments(): Observable<Department[]> {
    const apiUri: string = `${BASE_API_URI}/teacher-departments/api/read/all`;
    return this._httpClient.get<Department[]>(apiUri);
  }
}
