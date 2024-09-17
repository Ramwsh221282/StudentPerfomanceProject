import { Injectable } from '@angular/core';
import { BaseSemesterService } from './base-semester.service';
import { HttpParams } from '@angular/common/http';
import { SemesterPaginationService } from './semester-pagination.service';
import { IRequestParamsFactory } from '../../../../../models/RequestParamsFactory/irequest-params-factory.interface';
import { Semester } from '../models/semester.interface';

@Injectable({
  providedIn: 'any',
})
export class SemesterFetchService extends BaseSemesterService {
  private _semesters: Semester[];

  public constructor() {
    super();
  }

  public get semesters(): Semester[] {
    return this._semesters;
  }

  public fetch(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<Semester[]>(`${this.baseApiUri}/byPage`, { params })
      .subscribe((response) => {
        this._semesters = response;
      });
  }

  public fetchByFilter(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<Semester[]>(`${this.baseApiUri}/byFilter`, { params })
      .subscribe((response) => {
        this._semesters = response;
      });
  }

  public createFetchRequestParamsFactory(
    paginationService: SemesterPaginationService
  ): IRequestParamsFactory {
    return new FetchRequestParams(paginationService);
  }

  public createFilterRequestParamsFactory(
    paginationService: SemesterPaginationService,
    semester: Semester
  ): IRequestParamsFactory {
    return new FilterRequestParams(paginationService, semester);
  }
}

class FetchRequestParams implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(paginationService: SemesterPaginationService) {
    this._httpParams = new HttpParams()
      .set('Page', paginationService.currentPage)
      .set('PageSize', paginationService.pageSize);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}

class FilterRequestParams implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(
    paginationService: SemesterPaginationService,
    semester: Semester
  ) {
    this._httpParams = new HttpParams()
      .set('Page', paginationService.currentPage)
      .set('PageSize', paginationService.pageSize)
      .set('Semester.Number', semester.number)
      .set('Group.Name', semester.groupName);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}
