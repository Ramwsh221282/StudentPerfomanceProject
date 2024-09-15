import { Injectable } from '@angular/core';
import { SemesterPlanBaseService } from './semester-plan-base.service';
import { SemesterPlan } from '../models/semester-plan.interface';
import { Semester } from '../../models/semester.interface';
import { IRequestParamsFactory } from '../../../../../models/RequestParamsFactory/irequest-params-factory.interface';
import { HttpParams } from '@angular/common/http';
import { SemesterPlanPaginationService } from './semester-plan-pagination.service';

@Injectable({
  providedIn: 'root',
})
export class SemesterPlanFetchService extends SemesterPlanBaseService {
  private _semesterPlans: SemesterPlan[] = [];
  private _currentSemester: Semester = {} as Semester;
  private _isSemesterSet: boolean = false;

  constructor() {
    super();
  }

  public get semesterPlans(): SemesterPlan[] {
    return this._semesterPlans;
  }

  public get currentSemester(): Semester {
    return this._currentSemester;
  }

  public setSemester(semester: Semester): void {
    if (!this._isSemesterSet) {
      this._currentSemester = semester;
      this._isSemesterSet = true;
    }
  }

  public fetch(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<SemesterPlan[]>(`${this.baseApiUri}/byPage`, {
        params,
      })
      .subscribe((response) => {
        this._semesterPlans = response;
      });
  }

  public fetchByFilter(factory: IRequestParamsFactory) {
    const params = factory.Params;
    this.httpClient
      .get<SemesterPlan[]>(`${this.baseApiUri}/byFilter`, {
        params,
      })
      .subscribe((response) => {
        this._semesterPlans = response;
      });
  }

  public createFetchRequestParamFactory(
    paginationService: SemesterPlanPaginationService
  ): IRequestParamsFactory {
    return new FetchRequestParamFactory(
      paginationService,
      this._currentSemester
    );
  }

  public createFilterRequestParamFactory(
    paginationService: SemesterPlanPaginationService,
    plan: SemesterPlan
  ) {
    return new FilterRequestParamFactory(paginationService, plan);
  }
}

class FetchRequestParamFactory implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(
    paginationService: SemesterPlanPaginationService,
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

class FilterRequestParamFactory implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(
    paginationService: SemesterPlanPaginationService,
    plan: SemesterPlan
  ) {
    this._httpParams = new HttpParams()
      .set('Page', paginationService.currentPage)
      .set('PageSize', paginationService.pageSize)
      .set('Group.Name', plan.groupName)
      .set('Semester.Number', plan.semesterNumber)
      .set('Discipline.Name', plan.disciplineName);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}
