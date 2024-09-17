import { Injectable } from '@angular/core';
import { CreateTeacherService } from './create-teacher.service';
import { DeleteTeacherService } from './delete-teacher.service';
import { FetchTeacherService } from './fetch-teacher.service';
import { PaginationTeacherService } from './pagination-teacher.service';
import { SelectionTeacherService } from './selection-teacher.service';
import { UpdateTeacherService } from './update-teacher.service';
import { Observable } from 'rxjs';
import { Department } from '../../departments/models/departments.interface';
import { Teacher } from '../models/teacher.interface';
import { TeachersModule } from '../teachers.module';

@Injectable({
  providedIn: TeachersModule,
})
export class FacadeTeacherService {
  constructor(
    private readonly _createService: CreateTeacherService,
    private readonly _deleteService: DeleteTeacherService,
    private readonly _fetchService: FetchTeacherService,
    private readonly _paginationService: PaginationTeacherService,
    private readonly _selectionService: SelectionTeacherService,
    private readonly _updateService: UpdateTeacherService
  ) {}

  public get teachers(): Teacher[] {
    return this._fetchService.teachers;
  }

  public get currentDepartment(): Department {
    return this._fetchService.currentDepartment;
  }

  public get currentPage(): number {
    return this._paginationService.currentPage;
  }

  public get pages(): number[] {
    return this._paginationService.displayPages;
  }

  public get selected(): Teacher {
    return this._selectionService.selected;
  }

  public get selectedCopy(): Teacher {
    return this._selectionService.copy;
  }

  public get count(): number {
    return this._paginationService.totalCount;
  }

  public set setPage(page: number) {
    this._paginationService.selectPage(page);
    this.refreshPagination();
  }

  public set setSelection(teacher: Teacher) {
    this._selectionService.SetSelection = teacher;
  }

  public create(teacher: Teacher): Observable<Teacher> {
    const factory = this._createService.createRequestBodyFactory(
      teacher,
      this._fetchService.currentDepartment
    );
    return this._createService.create(factory);
  }

  public delete(teacher: Teacher): Observable<Teacher> {
    const factory = this._deleteService.createRequestBodyFactory(teacher);
    return this._deleteService.delete(factory);
  }

  public fetch(): void {
    const factory = this._fetchService.createFetchRequestParamsFactory(
      this._paginationService
    );
    this._fetchService.fetch(factory);
  }

  public filter(teacher: Teacher): void {
    const factory = this._fetchService.createFilterRequestParamFactory(
      this._paginationService,
      teacher
    );
    this._fetchService.fetchByFilter(factory);
  }

  public searchByDepartment(department: Department): Observable<Teacher[]> {
    const factory =
      this._fetchService.createByDepartmentRequestParamFactory(department);
    return this._fetchService.fetchByDepartmentName(factory);
  }

  public setDepartment(department: Department): void {
    this._fetchService.initialize(department);
  }

  public refreshPagination(): void {
    const factory = this._paginationService.createRequestParamsFactory(
      this._fetchService.currentDepartment
    );
    this._paginationService.refreshPagination(factory);
  }

  public refreshSelection(): void {
    this._selectionService.refreshSelection();
  }

  public update(newData: Teacher): Observable<Teacher> {
    const factory = this._updateService.createRequestBodyFactory(
      this._selectionService.copy,
      newData
    );
    return this._updateService.update(factory);
  }

  public moveNextPage(): void {
    this._paginationService.moveNextPage();
  }

  public movePreviousPage(): void {
    this._paginationService.movePreviousPage();
  }

  public moveLastPage(): void {
    this._paginationService.moveLastPage();
  }

  public moveInitialPage(): void {
    this._paginationService.moveInitialPage();
  }
}
