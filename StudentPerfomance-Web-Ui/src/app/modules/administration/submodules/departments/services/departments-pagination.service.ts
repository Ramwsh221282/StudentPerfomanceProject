import { Injectable } from '@angular/core';
import { DepartmentsBaseService } from './departments-base.service';
import { HttpParams } from '@angular/common/http';
import { IRequestParamsFactory } from '../../../../../models/RequestParamsFactory/irequest-params-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class DepartmentsPaginationService extends DepartmentsBaseService {
  private readonly _pageSize: number = 14;
  private _pagesCount: number = 0;
  private _totalCount: number = 0;
  private _currentPage: number = 1;
  private _displayPages: number[] = [];

  public constructor() {
    super();
  }

  public get totalCount(): number {
    return this._totalCount;
  }

  public get displayPages(): number[] {
    return this._displayPages;
  }

  public get currentPage(): number {
    return this._currentPage;
  }

  public get pageSize(): number {
    return this._pageSize;
  }

  public moveLastPage(): void {
    this._currentPage = this._pagesCount;
    this.refreshPagination();
  }

  public moveInitialPage(): void {
    this._currentPage = 1;
    this.refreshPagination();
  }

  public moveNextPage(): void {
    if (this._currentPage == this._pagesCount) {
      return;
    }
    this._currentPage += 1;
    this.refreshPagination();
  }

  public movePreviousPage(): void {
    if (this._currentPage == 1) {
      return;
    }
    this._currentPage -= 1;
    this.refreshPagination();
  }

  public selectPage(page: number) {
    this._currentPage = page;
    this.refreshPagination();
  }

  public refreshPagination(): void {
    this.httpClient
      .get<number>(`${this.readApiUri}count`)
      .subscribe((response) => {
        this._totalCount = response;
        this._pagesCount = Math.ceil(this._totalCount / this._pageSize);
        this._displayPages = [];
        let startPage = this._currentPage - 2;
        let endPage = this._currentPage + 2;
        if (startPage < 1) {
          startPage = 1;
          endPage = Math.min(this._totalCount, 5);
        }
        if (endPage > this._pagesCount) {
          endPage = this._pagesCount;
          startPage = Math.max(1, this._pagesCount - 4);
        }
        for (let i = startPage; i <= endPage; i++) {
          this._displayPages.push(i);
        }
      });
  }

  public createRequestParamsFactory() {
    return new RequestParamFactory(this._currentPage, this._pageSize);
  }
}

class RequestParamFactory implements IRequestParamsFactory {
  private readonly _params: HttpParams;

  public constructor(page: number, pageSize: number) {
    this._params = new HttpParams().set('Page', page).set('PageSize', pageSize);
  }

  public get Params(): HttpParams {
    return this._params;
  }
}
