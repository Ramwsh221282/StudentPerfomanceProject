import { Injectable } from '@angular/core';
import { StudentBaseService } from './student-base.service';
import { Student } from '../../models/student.interface';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { IRequestParamsFactory } from '../../../../../models/RequestParamsFactory/irequest-params-factory.interface';
import { HttpParams } from '@angular/common/http';
import { PaginationStudentsService } from './pagination-students.service';

@Injectable({
  providedIn: 'root',
})
export class FetchStudentService extends StudentBaseService {
  private _group: StudentGroup = {} as StudentGroup;
  private _isInitedOnce: boolean = false;
  private _students: Student[] = [];

  constructor() {
    super();
  }

  public get students(): Student[] {
    return this._students;
  }

  public get currentGroup(): StudentGroup {
    return this._group;
  }

  public initStudents(studentGroup: StudentGroup): void {
    if (!this._isInitedOnce) {
      this._group.groupName = studentGroup.groupName;
      this._isInitedOnce = true;
    }
  }

  public fetchStudents(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<Student[]>(`${this.baseApiUri}/byPage`, {
        params,
      })
      .subscribe((response) => {
        this._students = response;
      });
  }

  public filter(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<Student[]>(`${this.baseApiUri}/byFilter`, {
        params,
      })
      .subscribe((response) => (this._students = response));
  }

  public createFetchRequestParams(
    paginationService: PaginationStudentsService
  ): IRequestParamsFactory {
    return new FetchHttpRequestParam(paginationService, this._group);
  }

  public createFilterRequestParams(
    paginationService: PaginationStudentsService,
    student: Student
  ) {
    return new FilterHttpRequestParam(paginationService, student, this._group);
  }
}

class FetchHttpRequestParam implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(
    paginationService: PaginationStudentsService,
    group: StudentGroup
  ) {
    this._httpParams = new HttpParams()
      .set('page', paginationService.currentPage)
      .set('pageSize', paginationService.pageSize)
      .set('Group.Name', group.groupName);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}

class FilterHttpRequestParam implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(
    paginationService: PaginationStudentsService,
    student: Student,
    group: StudentGroup
  ) {
    this._httpParams = new HttpParams()
      .set('Page', paginationService.currentPage)
      .set('PageSize', paginationService.pageSize)
      .set('Student.Name', student.name)
      .set('Student.Surname', student.surname)
      .set('Student.Thirdname', student.thirdname)
      .set('Student.State', student.state)
      .set('Student.Recordbook', student.recordBook)
      .set('Group.Name', group.groupName);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}
