import { inject, Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpParams } from '@angular/common/http';
import { User } from '../../../../users/services/user-interface';
import { AuthService } from '../../../../users/services/auth.service';

@Injectable({
  providedIn: 'any',
})
export class PaginationService extends BaseService {
  private readonly _user: User;
  private _totalCount: number = 0;
  private _pageSize: number = 4;
  private _pagesCount: number = 0;
  private _currentPage: number = 1;
  private _displayPages: number[] = [];

  public constructor() {
    super();
    const authService = inject(AuthService);
    this._user = { ...authService.userData };
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

  public refreshPagination(): void {
    const params = this.buildRequestParams();
    this.httpClient
      .get<number>(`${this.readApiUri}count`, { params })
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
        this.pushFirstPage();
      });
  }

  private pushFirstPage() {
    if (this.displayPages.length == 0) {
      this.displayPages.push(1);
    }
  }

  private buildRequestParams(): HttpParams {
    const params: HttpParams = new HttpParams().set('token', this._user.token);
    return params;
  }
}