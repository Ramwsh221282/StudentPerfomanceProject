import { Injectable } from '@angular/core';
import { CreateService } from './create.service';
import { DeleteService } from './delete.service';
import { FetchService } from './fetch.service';
import { PaginationService } from './pagination.service';
import { UpdateService } from './update.service';
import { EducationDirection } from '../models/education-direction-interface';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../shared/models/fetch-policices/fetch-policy-interface';

@Injectable({
  providedIn: 'any',
})
export class FacadeService {
  constructor(
    private readonly _createService: CreateService,
    private readonly _deleteService: DeleteService,
    private readonly _paginationService: PaginationService,
    private readonly _fetchService: FetchService,
    private readonly _updateService: UpdateService
  ) {}

  public create(direction: EducationDirection): Observable<EducationDirection> {
    const factory = this._createService.createRequestBodyFactory(direction);
    return this._createService.create(factory);
  }

  public delete(direction: EducationDirection): Observable<EducationDirection> {
    const factory = this._deleteService.createRequestBodyFactory(direction);
    return this._deleteService.delete(factory);
  }

  public fetch(): void {
    this._fetchService.addPages(this.currentPage, this.pageSize);
    this._fetchService.fetch();
  }

  public setFetchPolicy(policy: IFetchPolicy<EducationDirection[]>) {
    this._fetchService.setPolicy(policy);
  }

  public update(
    oldDirection: EducationDirection,
    newDirection: EducationDirection
  ): Observable<EducationDirection> {
    const factory = this._updateService.createRequestBodyFactory(
      oldDirection,
      newDirection
    );
    return this._updateService.update(factory);
  }

  public refreshPagination(): void {
    this._paginationService.refreshPagination();
  }

  public get directions(): EducationDirection[] {
    return this._fetchService.directions;
  }

  public get currentPage(): number {
    return this._paginationService.currentPage;
  }

  public get pageSize(): number {
    return this._paginationService.pageSize;
  }

  public get displayPages(): number[] {
    return this._paginationService.displayPages;
  }

  public get count(): number {
    return this._paginationService.totalCount;
  }

  public set setPage(page: number) {
    this._paginationService.selectPage(page);
    this.fetch();
  }

  public movePreviousPage(): void {
    this._paginationService.movePreviousPage();
    this.fetch();
  }

  public moveNextPage(): void {
    this._paginationService.moveNextPage();
    this.fetch();
  }

  public moveLastPage(): void {
    this._paginationService.moveLastPage();
    this.fetch();
  }

  public moveInitialPage(): void {
    this._paginationService.moveInitialPage();
    this.fetch();
  }
}
