import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationPlan } from '../models/education-plan-interface';
import { IRequestParamsFactory } from '../../../../../models/RequestParamsFactory/irequest-params-factory.interface';
import { HttpParams } from '@angular/common/http';
import { PaginationService } from './pagination.service';

@Injectable({
  providedIn: 'any',
})
export class FetchService extends BaseService {
  private _plans: EducationPlan[] = [];
  public constructor() {
    super();
  }

  public get Plans(): EducationPlan[] {
    return this._plans;
  }

  public fetch(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<EducationPlan[]>(`${this.baseApiUri}/byPage`, { params })
      .subscribe((response) => (this._plans = response));
  }

  public filter(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<EducationPlan[]>(`${this.baseApiUri}/filter`, { params })
      .subscribe((response) => (this._plans = response));
  }

  public filterByYear(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<EducationPlan[]>(`${this.baseApiUri}/filterByYear`, { params })
      .subscribe((response) => (this._plans = response));
  }

  public filterByDirection(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<EducationPlan[]>(`${this.baseApiUri}/filterByDirection`, { params })
      .subscribe((response) => (this._plans = response));
  }

  public createFetchRequestParamsFactory(
    service: PaginationService
  ): IRequestParamsFactory {
    return new FetchRequestParamFactory(service.currentPage, service.pageSize);
  }

  public createFilterRequestParamsFactory(
    service: PaginationService,
    plan: EducationPlan
  ): IRequestParamsFactory {
    return new FilterRequestParamFactory(
      service.currentPage,
      service.pageSize,
      plan
    );
  }
}

class FetchRequestParamFactory implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(page: number, pageSize: number) {
    this._httpParams = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}

class FilterRequestParamFactory implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(page: number, pageSize: number, plan: EducationPlan) {
    this._httpParams = new HttpParams()
      .set('Plan.Year', plan.year)
      .set('Plan.Direction.Code', plan.direction.code)
      .set('Plan.Direction.Name', plan.direction.name)
      .set('Plan.Direction.Type', plan.direction.type)
      .set('page', page)
      .set('pageSize', pageSize);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}
