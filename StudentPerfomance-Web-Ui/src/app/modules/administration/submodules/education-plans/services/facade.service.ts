import { Injectable } from '@angular/core';
import { CreateService } from './create.service';
import { DeleteService } from './delete.service';
import { FetchService } from './fetch.service';
import { PaginationService } from './pagination.service';
import { Observable } from 'rxjs';
import { EducationPlan } from '../models/education-plan-interface';
import { IFetchPolicy } from '../../../../../shared/models/fetch-policices/fetch-policy-interface';

@Injectable({
  providedIn: 'any',
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
    this._fetchService.addPages(this.currentPage, this.pageSize);
    this._fetchService.fetch();
  }

  public setFetchPolicy(policy: IFetchPolicy<EducationPlan[]>): void {
    this._fetchService.setPolicy(policy);
  }

  public refreshPagination(): void {
    this._paginationService.refreshPagination();
  }

  public movePreviousPage(): void {
    this._paginationService.movePreviousPage();
    this.fetch();
  }

  public moveNextPage(): void {
    this._paginationService.moveNextPage();
    this.fetch();
  }

  public moveInitialPage(): void {
    this._paginationService.moveInitialPage();
    this.fetch();
  }

  public moveLastPage(): void {
    this._paginationService.moveLastPage();
    this.fetch();
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

  public get pageSize(): number {
    return this._paginationService.pageSize;
  }

  public get count(): number {
    return this._paginationService.totalCount;
  }

  public set setPage(page: number) {
    this._paginationService.selectPage(page);
    this.fetch();
  }
}
