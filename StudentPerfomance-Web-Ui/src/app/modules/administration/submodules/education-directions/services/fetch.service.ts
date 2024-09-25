import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EducationDirection } from '../models/education-direction-interface';
import { IRequestParamsFactory } from '../../../../../models/RequestParamsFactory/irequest-params-factory.interface';
import { HttpParams } from '@angular/common/http';
import { PaginationService } from './pagination.service';

@Injectable({
  providedIn: 'any',
})
export class FetchService extends BaseService {
  private _directions: EducationDirection[] = [];
  public constructor() {
    super();
  }

  public get directions(): EducationDirection[] {
    return this._directions;
  }

  public fetchPaged(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<EducationDirection[]>(`${this.baseApiUri}/byPage`, { params })
      .subscribe((response) => {
        this._directions = response;
      });
  }

  public fetchFilteredAndPaged(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<EducationDirection[]>(`${this.baseApiUri}/filter`, { params })
      .subscribe((response) => {
        this._directions = response;
      });
  }

  public createFetchPagedRequestParamsFactory(
    service: PaginationService
  ): IRequestParamsFactory {
    return new PagedRequestParamFactory(service.currentPage, service.pageSize);
  }

  public createPagedAndFilteredRequestParamsFactory(
    direction: EducationDirection,
    service: PaginationService
  ): IRequestParamsFactory {
    return new PagedFilterRequestParamFactory(
      direction,
      service.currentPage,
      service.pageSize
    );
  }
}

class PagedRequestParamFactory implements IRequestParamsFactory {
  private readonly _params: HttpParams;
  public constructor(page: number, pageSize: number) {
    this._params = new HttpParams().set('page', page).set('pageSize', pageSize);
  }
  public get Params(): HttpParams {
    return this._params;
  }
}

class PagedFilterRequestParamFactory implements IRequestParamsFactory {
  private readonly _params: HttpParams;
  public constructor(
    direction: EducationDirection,
    page: number,
    pageSize: number
  ) {
    this._params = new HttpParams()
      .set('Direction.Code', direction.code)
      .set('Direction.Name', direction.name)
      .set('Direction.Type', direction.type)
      .set('page', page)
      .set('pageSize', pageSize);
  }

  public get Params(): HttpParams {
    return this._params;
  }
}
