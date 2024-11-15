import { Injectable } from '@angular/core';
import { StudentGroupsService } from './student-groups-base-service';
import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../users/services/auth.service';
import { TokenPayloadBuilder } from '../../../../../shared/models/common/token-contract/token-payload-builder';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupsPaginationService extends StudentGroupsService {
  private readonly _apiUri: string = `${BASE_API_URI}/api/student-groups/count`;
  private _totalCount: number = 0;
  private _pageSize: number = 10;
  private _pagesCount: number = 0;
  private _currentPage: number = 1;
  private _displayPages: number[] = [];

  constructor(private readonly _authService: AuthService) {
    super();
  }

  public get currentPage(): number {
    return this._currentPage;
  }

  public get pageSize(): number {
    return this._pageSize;
  }

  public get displayPages(): number[] {
    return this._displayPages;
  }

  public get totalCount(): number {
    return this._totalCount;
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

  public refreshPagination() {
    const payload = {
      token: TokenPayloadBuilder(this._authService.userData),
    };
    this.httpClient
      .post<number>(this._apiUri, payload)
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
}
