import { Injectable } from '@angular/core';
import { SemesterCreateService } from './semester-create.service';
import { SemesterDeleteService } from './semester-delete.service';
import { SemesterFetchService } from './semester-fetch.service';
import { SemesterPaginationService } from './semester-pagination.service';
import { SemesterSelectionService } from './semester-selection.service';
import { Semester } from '../models/semester.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SemesterFacadeService {
  public constructor(
    private readonly _createService: SemesterCreateService,
    private readonly _deleteService: SemesterDeleteService,
    private readonly _fetchService: SemesterFetchService,
    private readonly _paginationService: SemesterPaginationService,
    private readonly _selectionService: SemesterSelectionService
  ) {}

  public get semesters(): Semester[] {
    return this._fetchService.semesters;
  }

  public get count(): number {
    return this._paginationService.totalCount;
  }

  public get currentPage(): number {
    return this._paginationService.currentPage;
  }

  public get pages(): number[] {
    return this._paginationService.displayPages;
  }

  public get selected(): Semester {
    return this._selectionService.selected;
  }

  public get selectedCopy(): Semester {
    return this._selectionService.copy;
  }

  public set setPage(page: number) {
    this._paginationService.selectPage(page);
    this.refreshPagination();
  }

  public set setSelection(semester: Semester) {
    this._selectionService.setSelection = semester;
  }

  public create(semester: Semester): Observable<Semester> {
    const factory = this._createService.createRequestBody(semester);
    return this._createService.create(factory);
  }

  public delete(semester: Semester): Observable<Semester> {
    const factory = this._deleteService.createRequestBodyFactory(semester);
    return this._deleteService.delete(factory);
  }

  public fetch(): void {
    const factory = this._fetchService.createFetchRequestParamsFactory(
      this._paginationService
    );
    this._fetchService.fetch(factory);
  }

  public filter(semester: Semester): void {
    const factory = this._fetchService.createFilterRequestParamsFactory(
      this._paginationService,
      semester
    );
    this._fetchService.fetch(factory);
  }

  public refreshPagination(): void {
    this._paginationService.refreshPagination();
  }

  public refreshSelection(): void {
    this._selectionService.refreshSelection();
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
}
