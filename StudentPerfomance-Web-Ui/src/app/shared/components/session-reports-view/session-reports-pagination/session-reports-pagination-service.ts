import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../../../modules/users/services/auth.service';
//import { BASE_API_URI } from '../../../models/api/api-constants';
import { AppConfigService } from '../../../../app.config.service';

@Injectable({
  providedIn: 'any',
})
export class SessionReportsPaginationService {
  private readonly _apiUri: string;
  private _totalCount: number = 0;
  private _pageSize: number = 12;
  private _pagesCount: number = 0;
  private _currentPage: number = 1;
  private _displayPages: number[] = [];

  constructor(
    private readonly _authService: AuthService,
    private readonly _httpClient: HttpClient,
    private readonly _appConfig: AppConfigService,
  ) {
    //this._apiUri = `${BASE_API_URI}/app/assignment-sessions/count`;
    this._apiUri = `${_appConfig.baseApiUri}/app/assignment-sessions/count`;
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
    const headers = this.buildHttpHeaders();
    this._httpClient
      .get<number>(this._apiUri, { headers: headers })
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

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }

  private pushFirstPage() {
    if (this.displayPages.length == 0) {
      this.displayPages.push(1);
    }
  }
}
