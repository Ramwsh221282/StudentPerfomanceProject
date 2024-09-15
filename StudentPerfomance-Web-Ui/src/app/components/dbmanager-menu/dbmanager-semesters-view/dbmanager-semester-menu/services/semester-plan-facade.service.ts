import { Injectable } from '@angular/core';
import { SemesterPlanCreateService } from './semester-plan-create.service';
import { SemesterPlanDeleteService } from './semester-plan-delete.service';
import { SemesterPlanFetchService } from './semester-plan-fetch.service';
import { SemesterPlanPaginationService } from './semester-plan-pagination.service';
import { SemesterPlanSelectionService } from './semester-plan-selection.service';
import { Semester } from '../../models/semester.interface';
import { SemesterPlan } from '../models/semester-plan.interface';
import { Observable } from 'rxjs';
import { Teacher } from '../../../dbmanager-departments-view/department-menu/models/teacher.interface';
import { SemesterPlanSetTeacherService } from './semester-plan-set-teacher.service';

@Injectable({
  providedIn: 'root',
})
export class SemesterPlanFacadeService {
  public constructor(
    private readonly _createService: SemesterPlanCreateService,
    private readonly _deleteService: SemesterPlanDeleteService,
    private readonly _fetchService: SemesterPlanFetchService,
    private readonly _paginationService: SemesterPlanPaginationService,
    private readonly _selectionService: SemesterPlanSelectionService,
    private readonly _assignService: SemesterPlanSetTeacherService
  ) {}

  public get count(): number {
    return this._paginationService.totalCount;
  }

  public get plans(): SemesterPlan[] {
    return this._fetchService.semesterPlans;
  }

  public get currentSemester(): Semester {
    return this._fetchService.currentSemester;
  }

  public get pages(): number[] {
    return this._paginationService.displayPages;
  }

  public get currentPage(): number {
    return this._paginationService.currentPage;
  }

  public get selected(): SemesterPlan {
    return this._selectionService.selected;
  }

  public get selectedCopy(): SemesterPlan {
    return this._selectionService.selectedCopy;
  }

  public set setPage(page: number) {
    this._paginationService.selectPage(page);
    this.refreshPagination();
  }

  public set setSelection(plan: SemesterPlan) {
    this._selectionService.setSelection = plan;
  }

  public initialize(semester: Semester): void {
    this._fetchService.setSemester(semester);
  }

  public create(plan: SemesterPlan): Observable<SemesterPlan> {
    const factory = this._createService.createRequestBody(plan);
    return this._createService.create(factory);
  }

  public delete(plan: SemesterPlan): Observable<SemesterPlan> {
    const factory = this._deleteService.createRequestBodyFactory(plan);
    return this._deleteService.delete(factory);
  }

  public refreshPagination(): void {
    const factory = this._paginationService.createRequestParamFactory(
      this._fetchService.currentSemester
    );
    this._paginationService.refreshPagination(factory);
  }

  public fetch(): void {
    const factory = this._fetchService.createFetchRequestParamFactory(
      this._paginationService
    );
    this._fetchService.fetch(factory);
  }

  public filter(plan: SemesterPlan): void {
    const factory = this._fetchService.createFilterRequestParamFactory(
      this._paginationService,
      plan
    );
    this._fetchService.fetchByFilter(factory);
  }

  public moveNextPage(): void {
    this._paginationService.moveNextPage();
    this.refreshPagination();
  }

  public movePreviousPage(): void {
    this._paginationService.movePreviousPage();
    this.refreshPagination();
  }

  public moveInitialPage(): void {
    this._paginationService.moveInitialPage();
    this.refreshPagination();
  }

  public moveLastPage(): void {
    this._paginationService.moveLastPage();
    this.refreshPagination();
  }

  public refreshSelection(): void {
    this._selectionService.clearSelection();
  }

  public assignTeacher(
    teacher: Teacher,
    plan: SemesterPlan
  ): Observable<SemesterPlan> {
    const factory = this._assignService.createRequestParamFactory(
      teacher,
      plan
    );
    return this._assignService.assign(factory);
  }
}
