import { inject, Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { User } from '../../../../users/services/user-interface';
import { AuthService } from '../../../../users/services/auth.service';
import { HttpHeaders } from '@angular/common/http';
import { AppConfigService } from '../../../../../app.config.service';

//import { BASE_API_URI } from '../../../../../shared/models/api/api-constants';

@Injectable({
  providedIn: 'any',
})
export class PaginationService extends BaseService {
  private _totalCount: number = 0;
  private _pageSize: number = 9;
  private _pagesCount: number = 0;
  private _currentPage: number = 1;
  private _displayPages: number[] = [];
  private readonly _user: User;

  public constructor(private readonly _appConfig: AppConfigService) {
    super();
    const authService = inject(AuthService);
    this._user = { ...authService.userData };
    this.pushFirstPage();
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
    //const apiUri = `${BASE_API_URI}/api/education-direction/count`;
    const apiUri = `${this._appConfig.baseApiUri}/api/education-direction/count`;
    const headers = this.buildHttpHeaders();
    this.httpClient
      .get<number>(apiUri, {
        headers: headers,
      })
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
    const headers: HttpHeaders = new HttpHeaders().set(
      'token',
      this._user.token,
    );
    return headers;
  }

  private pushFirstPage() {
    if (this.displayPages.length == 0) {
      this.displayPages.push(1);
    }
  }
}
