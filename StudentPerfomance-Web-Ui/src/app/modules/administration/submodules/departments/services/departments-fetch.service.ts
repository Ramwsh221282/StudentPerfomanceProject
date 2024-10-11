import { Injectable } from '@angular/core';
import { DepartmentsBaseService } from './departments-base.service';
import { HttpParams } from '@angular/common/http';
import { DepartmentsPaginationService } from './departments-pagination.service';
import { Observable } from 'rxjs';
import { Department } from '../models/departments.interface';
import { IRequestParamsFactory } from '../../../../../models/RequestParamsFactory/irequest-params-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class DepartmentsFetchService extends DepartmentsBaseService {
  private _departments: Department[] = [];

  constructor() {
    super();
  }

  public get departments(): Department[] {
    return this._departments;
  }

  public fetchAll(): Observable<Department[]> {
    return this.httpClient.get<Department[]>(`${this.readApiUri}all`);
  }

  public fetchPaged(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<Department[]>(`${this.readApiUri}byPage`, { params })
      .subscribe((response) => {
        this._departments = response;
      });
  }

  public filter(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<Department[]>(`${this.readApiUri}filter`, {
        params,
      })
      .subscribe((response) => {
        this._departments = response;
      });
  }

  public createFetchByPageRequestParamsFactory(
    paginationService: DepartmentsPaginationService
  ): IRequestParamsFactory {
    return new FetchByPageRequestParamFactory(paginationService);
  }

  public createFilterRequestParamFactory(
    paginationService: DepartmentsPaginationService,
    department: Department
  ): IRequestParamsFactory {
    return new FilterRequestParamFactory(paginationService, department);
  }
}

class FetchByPageRequestParamFactory implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(paginationService: DepartmentsPaginationService) {
    this._httpParams = new HttpParams()
      .set('Page', paginationService.currentPage)
      .set('PageSize', paginationService.pageSize);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}

class FilterRequestParamFactory implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(
    paginationService: DepartmentsPaginationService,
    department: Department
  ) {
    this._httpParams = new HttpParams()
      .set('Page', paginationService.currentPage)
      .set('PageSize', paginationService.pageSize)
      .set('Department.Name', department.name);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}
