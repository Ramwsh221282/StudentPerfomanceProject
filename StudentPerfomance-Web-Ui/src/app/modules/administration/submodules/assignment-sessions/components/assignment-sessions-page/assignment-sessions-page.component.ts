import { Component, OnInit } from '@angular/core';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { AssignmentSessionDataService } from '../../services/assignment-session-data.service';
import { AssignmentSession } from '../../models/assignment-session-interface';
import { AssignmentSessionPaginationService } from '../../services/assignment-session-pagination.service';
import { AssignmentSessionDefaultFetchPolicy } from '../../models/fetch-policies/assignment-session-default-fetch-policy';
import { AuthService } from '../../../../../../pages/user-page/services/auth.service';
import { AppConfigService } from '../../../../../../app.config.service';
import { AssignmentSessionCloseService } from '../../services/assignment-session-close.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-assignment-sessions-page',
  templateUrl: './assignment-sessions-page.component.html',
  styleUrl: './assignment-sessions-page.component.scss',
  providers: [
    UserOperationNotificationService,
    AssignmentSessionDataService,
    AssignmentSessionPaginationService,
    AssignmentSessionCloseService,
  ],
})
export class AssignmentSessionsPageComponent implements OnInit {
  protected currentAssignmentSession: AssignmentSession | null;

  protected assignmentSessionToClose: AssignmentSession | null;
  protected isClosingAssignmentSession: boolean = false;

  public constructor(
    protected readonly notificationService: UserOperationNotificationService,
    private readonly _dataService: AssignmentSessionDataService,
    private readonly _paginationService: AssignmentSessionPaginationService,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
    private readonly _closeService: AssignmentSessionCloseService,
  ) {}

  public ngOnInit(): void {
    this._paginationService.refreshPagination();
    const policy = new AssignmentSessionDefaultFetchPolicy(
      this._authService,
      this._appConfig,
    );
    policy.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this._dataService.setPolicy(policy);
    this._dataService.fetch().subscribe((response) => {
      if (response.length != 0) {
        this.currentAssignmentSession = response[0];
      }
    });
  }

  protected handleSessionClose(session: AssignmentSession): void {
    this._closeService
      .close(session)
      .pipe(
        tap((response) => {
          this.notificationService.SetMessage =
            'Контрольная неделя закрыта и сформирован отчёт';
          this.notificationService.success();
          this.ngOnInit();
        }),
        catchError((error: HttpErrorResponse) => {
          this.notificationService.SetMessage = error.error;
          this.notificationService.failure();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }
}
