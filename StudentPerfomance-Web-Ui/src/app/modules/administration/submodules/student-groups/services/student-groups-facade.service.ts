import { Injectable } from '@angular/core';
import { StudentGroupSearchService } from './student-group-search.service';
import { StudentGroupsCreateDataService } from './student-groups-create-data.service';
import { StudentGroupsDeleteDataService } from './student-groups-delete-data.service';
import { StudentGroupsFetchDataService } from './student-groups-fetch-data.service';
import { StudentGroupsMergeDataService } from './student-groups-merge-data.service';
import { StudentGroupsPaginationService } from './student-groups-pagination.service';
import { StudentGroupsUpdateDataService } from './student-groups-update-data.service';
import { StudentGroup } from './studentsGroup.interface';
import { Observable } from 'rxjs';
import { StudentGroupsSelectionService } from './student-groups-selection.service';
import { StudentGroupsModule } from '../student-groups.module';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupsFacadeService {
  public constructor(
    private readonly _searchService: StudentGroupSearchService,
    private readonly _createService: StudentGroupsCreateDataService,
    private readonly _deleteService: StudentGroupsDeleteDataService,
    private readonly _fetchService: StudentGroupsFetchDataService,
    private readonly _mergeService: StudentGroupsMergeDataService,
    private readonly _paginationService: StudentGroupsPaginationService,
    private readonly _updateService: StudentGroupsUpdateDataService,
    private readonly _selectionService: StudentGroupsSelectionService
  ) {}

  public create(group: StudentGroup): Observable<StudentGroup> {
    const factory = this._createService.createRequestBodyFactory(group);
    return this._createService.create(factory);
  }

  public delete(group: StudentGroup): Observable<StudentGroup> {
    const factory = this._createService.createRequestBodyFactory(group);
    return this._deleteService.delete(factory);
  }

  public fetchData(): void {
    const factory = this._paginationService.createPaginationRequestFactory();
    this._fetchService.fetch(factory);
  }

  public refreshPagination(): void {
    this._paginationService.refreshPagination();
  }

  public filterData(group: StudentGroup) {
    const factory = this._fetchService.createFilterRequestFactory(
      group,
      this._paginationService.currentPage,
      this._paginationService.pageSize
    );
    this._fetchService.filter(factory);
  }

  public merge(
    groupA: StudentGroup,
    groupB: StudentGroup
  ): Observable<StudentGroup> {
    const factory = this._mergeService.createRequestBodyFactory(groupA, groupB);
    return this._mergeService.merge(factory);
  }

  public update(group: StudentGroup): Observable<StudentGroup> {
    const factory = this._updateService.requestBodyFactory(
      this._selectionService.copy,
      group
    );
    return this._updateService.update(factory);
  }

  public clear(): void {
    this._selectionService.clear();
  }

  public search(group: StudentGroup): Observable<StudentGroup[]> {
    const factory = this._searchService.createRequestParamsFactory(group);
    return this._searchService.searchByName(factory);
  }

  public moveNextPage(): void {
    this._paginationService.moveNextPage();
    this.fetchData();
  }

  public movePreviousPage(): void {
    this._paginationService.movePreviousPage();
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

  public set select(group: StudentGroup) {
    this._selectionService.set = group;
  }

  public get selected(): StudentGroup {
    return this._selectionService.selected;
  }

  public get copy(): StudentGroup {
    return this._selectionService.copy;
  }

  public get groups(): StudentGroup[] {
    return this._fetchService.studentGroups;
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

  public set page(page: number) {
    this._paginationService.selectPage(page);
    this.fetchData();
  }
}
