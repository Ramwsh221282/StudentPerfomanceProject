import { Injectable } from '@angular/core';
import { BaseTeacherService } from './base-teacher.service';
import { HttpParams } from '@angular/common/http';
import { PaginationTeacherService } from './pagination-teacher.service';
import { Department } from '../../departments/models/departments.interface';
import { Teacher } from '../models/teacher.interface';
import { TeachersModule } from '../teachers.module';
import { IRequestParamsFactory } from '../../../../../models/RequestParamsFactory/irequest-params-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class FetchTeacherService extends BaseTeacherService {
  private _teachers: Teacher[] = [];
  private _currentDepartment: Department = {} as Department;
  private _isInited: boolean = false;

  public constructor() {
    super();
  }

  public get teachers(): Teacher[] {
    return this._teachers;
  }

  public get currentDepartment(): Department {
    return this._currentDepartment;
  }

  public initialize(department: Department) {
    if (!this._isInited) {
      this._currentDepartment = { ...department };
      this._isInited = true;
    }
  }

  public fetchByDepartmentName(factory: IRequestParamsFactory) {
    const params = factory.Params;
    return this.httpClient.get<Teacher[]>(`${this.baseApiUri}/byDepartment`, {
      params,
    });
  }

  public fetch(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<Teacher[]>(`${this.baseApiUri}/byPage`, { params })
      .subscribe((response) => {
        this._teachers = response;
      });
  }

  public fetchByFilter(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<Teacher[]>(`${this.baseApiUri}/byFilter`, { params })
      .subscribe((response) => {
        this._teachers = response;
      });
  }

  public createFetchRequestParamsFactory(
    paginationService: PaginationTeacherService
  ): IRequestParamsFactory {
    return new FetchRequestParamFactory(
      paginationService,
      this._currentDepartment
    );
  }

  public createFilterRequestParamFactory(
    paginationService: PaginationTeacherService,
    teacher: Teacher
  ): IRequestParamsFactory {
    return new FilterRequestParamFactory(
      paginationService,
      teacher,
      this._currentDepartment
    );
  }

  public createByDepartmentRequestParamFactory(department: Department) {
    return new ByDepartmentRequestParam(department);
  }
}

class FetchRequestParamFactory implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(
    paginationService: PaginationTeacherService,
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

class FilterRequestParamFactory implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(
    paginationService: PaginationTeacherService,
    teacher: Teacher,
    department: Department
  ) {
    this._httpParams = new HttpParams()
      .set('Page', paginationService.currentPage)
      .set('PageSize', paginationService.pageSize)
      .set('Teacher.Name', teacher.name)
      .set('Teacher.Surname', teacher.surname)
      .set('Teacher.Thirdname', teacher.thirdname)
      .set('Department.Name', department.name);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}

class ByDepartmentRequestParam implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(department: Department) {
    this._httpParams = new HttpParams().set('Department.Name', department.name);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}
