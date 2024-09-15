import { Injectable } from '@angular/core';
import { DepartmentsCreateService } from './departments-create.service';
import { DepartmentsDeleteService } from './departments-delete.service';
import { DepartmentsFetchService } from './departments-fetch.service';
import { DepartmentsPaginationService } from './departments-pagination.service';
import { DepartmentsUpdateService } from './departments-update.service';
import { DepartmentsSelectionService } from './departments-selection.service';
import { Department } from './departments.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DepartmentsFacadeService {
  public constructor(
    private readonly _createService: DepartmentsCreateService,
    private readonly _deleteService: DepartmentsDeleteService,
    private readonly _fetchService: DepartmentsFetchService,
    private readonly _paginationService: DepartmentsPaginationService,
    private readonly _updateService: DepartmentsUpdateService,
    private readonly _selectionService: DepartmentsSelectionService
  ) {}

  public get departments(): Department[] {
    return this._fetchService.departments;
  }

  public get count(): number {
    return this._paginationService.totalCount;
  }

  public get pages(): number[] {
    return this._paginationService.displayPages;
  }

  public get currentPage(): number {
    return this._paginationService.currentPage;
  }

  public get selected(): Department {
    return this._selectionService.selected;
  }

  public get selectedCopy(): Department {
    return this._selectionService.copy;
  }

  public set setSelection(department: Department) {
    this._selectionService.setSelection = department;
  }

  public set setPage(page: number) {
    this._paginationService.selectPage(page);
  }

  public create(department: Department): Observable<Department> {
    const factory = this._createService.createRequestBodyFactory(department);
    return this._createService.create(factory);
  }

  public delete(department: Department): Observable<Department> {
    const factory = this._deleteService.createRequestBodyFactory(department);
    return this._deleteService.delete(factory);
  }

  public fetchData(): void {
    const factory = this._fetchService.createFetchByPageRequestParamsFactory(
      this._paginationService
    );
    this._fetchService.fetchPaged(factory);
  }

  public filterData(department: Department): void {
    const factory = this._fetchService.createFilterRequestParamFactory(
      this._paginationService,
      department
    );
    this._fetchService.filter(factory);
  }

  public fetchAllData(): Observable<Department[]> {
    return this._fetchService.fetchAll();
  }

  public refreshPagination(): void {
    this._paginationService.refreshPagination();
  }

  public refreshSelection(): void {
    this._selectionService.refreshSelection();
  }

  public update(department: Department): Observable<Department> {
    const factory = this._updateService.createRequestBodyFactory(
      this._selectionService.copy,
      department
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
