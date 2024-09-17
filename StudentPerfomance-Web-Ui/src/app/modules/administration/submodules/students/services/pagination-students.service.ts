import { Injectable } from '@angular/core';
import { StudentBaseService } from './student-base.service';
import { HttpParams } from '@angular/common/http';
import { StudentGroup } from '../../student-groups/services/studentsGroup.interface';
import { IRequestParamsFactory } from '../../../../../models/RequestParamsFactory/irequest-params-factory.interface';

@Injectable({
  providedIn: 'any',
})
export class PaginationStudentsService extends StudentBaseService {
  private _totalCount: number = 0;
  private _pageSize: number = 14;
  private _pagesCount: number = 0;
  private _currentPage: number = 1;
  private _displayPages: number[] = [];

  constructor() {
    super();
  }

  public get pageSize(): number {
    return this._pageSize;
  }

  public get currentPage(): number {
    return this._currentPage;
  }

  public get totalCount(): number {
    return this._totalCount;
  }

  public moveLastPage(): void {
    this._currentPage = this._pagesCount;
  }

  public moveInitialPage(): void {
    this._currentPage = 1;
  }

  public moveNextPage(): void {
    if (this._currentPage == this._pagesCount) {
      return;
    }
    this._currentPage += 1;
  }

  public movePreviousPage(): void {
    if (this._currentPage == 1) {
      return;
    }
    this._currentPage -= 1;
  }

  public selectPage(page: number) {
    this._currentPage = page;
  }

  public get displayPages(): number[] {
    return this._displayPages;
  }

  public refreshPagination(factory: IRequestParamsFactory): void {
    const params = factory.Params;
    this.httpClient
      .get<number>(`${this.baseApiUri}/totalCount`, { params })
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

  public createRequestParamFactory(group: StudentGroup): IRequestParamsFactory {
    return new HttpRequestParam(group);
  }
}

class HttpRequestParam implements IRequestParamsFactory {
  private readonly _httpParams: HttpParams;

  public constructor(group: StudentGroup) {
    this._httpParams = new HttpParams().set('Group.Name', group.groupName);
  }

  public get Params(): HttpParams {
    return this._httpParams;
  }
}
