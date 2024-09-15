import { Injectable } from '@angular/core';
import { CreateStudentService } from './create-student.service';
import { DeleteStudentService } from './delete-student.service';
import { FetchStudentService } from './fetch-student.service';
import { PaginationStudentsService } from './pagination-students.service';
import { SelectionStudentService } from './selection-student.service';
import { UpdateStudentService } from './update-student.service';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { Student } from '../../models/student.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FacadeStudentService {
  public constructor(
    private readonly _createService: CreateStudentService,
    private readonly _deleteService: DeleteStudentService,
    private readonly _fetchService: FetchStudentService,
    private readonly _paginationService: PaginationStudentsService,
    private readonly _selectionService: SelectionStudentService,
    private readonly _updateService: UpdateStudentService
  ) {}

  public setStudentGroup(group: StudentGroup) {
    this._fetchService.initStudents(group);
  }

  public create(student: Student): Observable<Student> {
    const factory = this._createService.createRequestBodyFactory(student);
    return this._createService.create(factory);
  }

  public delete(student: Student): Observable<Student> {
    const factory = this._deleteService.createRequestBodyFactory(student);
    return this._deleteService.delete(factory);
  }

  public fetchData(): void {
    const factory = this._fetchService.createFetchRequestParams(
      this._paginationService
    );
    this._fetchService.fetchStudents(factory);
  }

  public filterData(student: Student): void {
    const factory = this._fetchService.createFilterRequestParams(
      this._paginationService,
      student
    );
    this._fetchService.filter(factory);
  }

  public refreshPagination(): void {
    const factory = this._paginationService.createRequestParamFactory(
      this.currentGroup
    );
    this._paginationService.refreshPagination(factory);
  }

  public movePreviousPage(): void {
    this._paginationService.movePreviousPage();
    this.fetchData();
  }

  public moveNextPage(): void {
    this._paginationService.moveNextPage();
    this.fetchData();
  }

  public moveLastPage(): void {
    this._paginationService.moveLastPage();
    this.fetchData();
  }

  public moveInitialPage(): void {
    this._paginationService.moveInitialPage();
    this.fetchData();
  }

  public get copy(): Student {
    return this._selectionService.copy;
  }

  public get selected(): Student {
    return this._selectionService.selected;
  }

  public set select(student: Student) {
    this._selectionService.set = student;
  }

  public refreshSelection(): void {
    this._selectionService.clear();
  }

  public set setPage(page: number) {
    this._paginationService.selectPage(page);
    this.fetchData();
  }

  public get count(): number {
    return this._paginationService.totalCount;
  }

  public get students(): Student[] {
    return this._fetchService.students;
  }

  public get pages(): number[] {
    return this._paginationService.displayPages;
  }

  public get currentPage(): number {
    return this._paginationService.currentPage;
  }

  public get currentGroup(): StudentGroup {
    return this._fetchService.currentGroup;
  }

  public update(student: Student): Observable<Student> {
    const factory = this._updateService.createRequestBodyFactory(
      this._selectionService.copy,
      student,
      this.currentGroup
    );
    return this._updateService.update(factory);
  }
}
