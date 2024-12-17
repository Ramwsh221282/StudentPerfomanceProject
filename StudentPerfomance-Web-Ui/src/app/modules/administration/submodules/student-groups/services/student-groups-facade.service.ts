import { Injectable } from '@angular/core';
import { StudentGroupsCreateDataService } from './student-groups-create-data.service';
import { StudentGroupsDeleteDataService } from './student-groups-delete-data.service';
import { StudentGroupsFetchDataService } from './student-groups-fetch-data.service';
import { StudentGroupsMergeDataService } from './student-groups-merge-data.service';
import { StudentGroupsPaginationService } from './student-groups-pagination.service';
import { StudentGroupsUpdateDataService } from './student-groups-update-data.service';
import { StudentGroup } from './studentsGroup.interface';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../shared/models/fetch-policices/fetch-policy-interface';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupsFacadeService {
  public constructor(
    private readonly _createService: StudentGroupsCreateDataService,
    private readonly _deleteService: StudentGroupsDeleteDataService,
    private readonly _fetchService: StudentGroupsFetchDataService,
    private readonly _mergeService: StudentGroupsMergeDataService,
    private readonly _paginationService: StudentGroupsPaginationService,
    private readonly _updateService: StudentGroupsUpdateDataService,
  ) {}

  public create(group: StudentGroup): Observable<StudentGroup> {
    return this._createService.create(group);
  }

  public delete(group: StudentGroup): Observable<StudentGroup> {
    return this._deleteService.delete(group);
  }

  public fetchData(): Observable<StudentGroup[]> {
    this._fetchService.addPages(this.currentPage, this.pageSize);
    return this._fetchService.fetch();
  }

  public setPolicy(policy: IFetchPolicy<StudentGroup[]>): void {
    this._fetchService.setPolicy(policy);
  }

  public refreshPagination(): void {
    this._paginationService.refreshPagination();
  }

  public merge(
    groupA: StudentGroup,
    groupB: StudentGroup,
  ): Observable<StudentGroup> {
    return this._mergeService.merge(groupA, groupB);
  }

  public update(
    initial: StudentGroup,
    newGroup: StudentGroup,
  ): Observable<StudentGroup> {
    return this._updateService.update(initial, newGroup);
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

  public get currentPage(): number {
    return this._paginationService.currentPage;
  }

  public get pageSize(): number {
    return this._paginationService.pageSize;
  }

  public get pages(): number[] {
    return this._paginationService.displayPages;
  }

  public set page(page: number) {
    this._paginationService.selectPage(page);
    this.fetchData();
  }
}
