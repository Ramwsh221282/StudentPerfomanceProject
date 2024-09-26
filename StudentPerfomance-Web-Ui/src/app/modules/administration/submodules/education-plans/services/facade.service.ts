import { Injectable } from '@angular/core';
import { EducationPlansModule } from '../education-plans.module';
import { CreateService } from './create.service';
import { DeleteService } from './delete.service';
import { FetchService } from './fetch.service';
import { PaginationService } from './pagination.service';
import { Observable } from 'rxjs';
import { EducationPlan } from '../models/education-plan-interface';

@Injectable({
  providedIn: EducationPlansModule,
})
export class FacadeService {
  public constructor(
    private readonly _createService: CreateService,
    private readonly _deleteService: DeleteService,
    private readonly _fetchService: FetchService,
    private readonly _paginationService: PaginationService
  ) {}

  public create(plan: EducationPlan): Observable<EducationPlan> {
    const factory = this._createService.createRequestBodyFactory(plan);
    return this._createService.create(factory);
  }

  public delete(plan: EducationPlan): Observable<EducationPlan> {
    const factory = this._deleteService.createRequestBodyFactory(plan);
    return this._deleteService.delete(factory);
  }

  public fetch(): void {
    const factory = this._fetchService.createFetchRequestParamsFactory(
      this._paginationService
    );
    this._fetchService.fetch(factory);
  }

  public filterByYear(plan: EducationPlan): void {
    const factory = this._fetchService.createFilterRequestParamsFactory(
      this._paginationService,
      plan
    );
    this._fetchService.filterByYear(factory);
  }

  public filterByDirection(plan: EducationPlan): void {
    const factory = this._fetchService.createFilterRequestParamsFactory(
      this._paginationService,
      plan
    );
    this._fetchService.filterByDirection(factory);
  }

  public refreshPagination(): void {
    this._paginationService.refreshPagination();
  }

  public movePreviousPage(): void {
    this._paginationService.movePreviousPage();
    this._paginationService.refreshPagination();
  }

  public moveNextPage(): void {
    this._paginationService.moveNextPage();
    this._paginationService.refreshPagination();
  }

  public moveInitialPage(): void {
    this._paginationService.moveInitialPage();
    this._paginationService.refreshPagination();
  }

  public moveLastPage(): void {
    this._paginationService.moveLastPage();
    this._paginationService.refreshPagination();
  }

  public get plans(): EducationPlan[] {
    return this._fetchService.Plans;
  }

  public get pages(): number[] {
    return this._paginationService.displayPages;
  }

  public get currentPage(): number {
    return this._paginationService.currentPage;
  }

  public get count(): number {
    return this._paginationService.totalCount;
  }

  public set setPage(page: number) {
    this._paginationService.selectPage(page);
    this._paginationService.refreshPagination();
  }
}
