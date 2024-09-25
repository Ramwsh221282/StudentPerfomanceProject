import { Injectable } from '@angular/core';
import { EducationDirectionsModule } from '../education-directions.module';
import { CreateService } from './create.service';
import { DeleteService } from './delete.service';
import { FetchService } from './fetch.service';
import { PaginationService } from './pagination.service';
import { SelectionService } from './selection.service';
import { UpdateService } from './update.service';
import { EducationDirection } from '../models/education-direction-interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: EducationDirectionsModule,
})
export class FacadeService {
  constructor(
    private readonly _createService: CreateService,
    private readonly _deleteService: DeleteService,
    private readonly _fetchService: FetchService,
    private readonly _paginationService: PaginationService,
    private readonly _selectionService: SelectionService,
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
    const factory = this._fetchService.createFetchPagedRequestParamsFactory(
      this._paginationService
    );
    this._fetchService.fetchPaged(factory);
  }

  public filter(direction: EducationDirection): void {
    const factory =
      this._fetchService.createPagedAndFilteredRequestParamsFactory(
        direction,
        this._paginationService
      );
    this._fetchService.fetchFilteredAndPaged(factory);
  }

  public update(
    oldDirection: EducationDirection,
    newDirection: EducationDirection
  ): Observable<EducationDirection> {
    console.log(oldDirection);
    const factory = this._updateService.createRequestBodyFactory(
      oldDirection,
      newDirection
    );
    return this._updateService.update(factory);
  }

  public refreshSelection(): void {
    this._selectionService.refreshSelection();
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

  public get displayPages(): number[] {
    return this._paginationService.displayPages;
  }

  public get count(): number {
    return this._paginationService.totalCount;
  }

  public get selected(): EducationDirection {
    return this._selectionService.Selected;
  }

  public get copy(): EducationDirection {
    return this._selectionService.Copy;
  }

  public set setSelection(direction: EducationDirection) {
    this._selectionService.Select = direction;
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
