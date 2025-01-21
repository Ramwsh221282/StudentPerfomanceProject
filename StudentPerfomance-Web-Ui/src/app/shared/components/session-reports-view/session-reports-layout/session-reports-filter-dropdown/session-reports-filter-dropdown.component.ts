import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FloatingLabelInputComponent } from '../../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { DropdownListComponent } from '../../../../../building-blocks/dropdown-list/dropdown-list.component';
import { NgIf } from '@angular/common';
import { SessionReportsDataService } from '../../Services/data-services/session-reports-data-service';
import { UserOperationNotificationService } from '../../../../services/user-notifications/user-operation-notification-service.service';
import { IsNullOrWhiteSpace } from '../../../../utils/string-helper';
import { SessionReportsPaginationService } from '../../session-reports-pagination/session-reports-pagination-service';
import { IFetchPolicy } from '../../../../models/fetch-policices/fetch-policy-interface';
import { ControlWeekReportInterface } from '../../Models/Data/control-week-report-interface';
import { SessionReportPeriodFetchPolicy } from '../../Services/data-services/reports-fetch-policies/session-report-period-fetch-policy';
import { AuthService } from '../../../../../pages/user-page/services/auth.service';
import { AppConfigService } from '../../../../../app.config.service';
import { SessionReportDefaultFetchPolicy } from '../../Services/data-services/reports-fetch-policies/session-report-default-fetch-policy';

@Component({
  selector: 'app-session-reports-filter-dropdown',
  standalone: true,
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    DropdownListComponent,
    NgIf,
  ],
  templateUrl: './session-reports-filter-dropdown.component.html',
  styleUrl: './session-reports-filter-dropdown.component.scss',
})
export class SessionReportsFilterDropdownComponent {
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() filterCommited: EventEmitter<void> = new EventEmitter();

  protected year: string = '';

  protected selectNumberLabel = 'Выберите номер';
  protected number: string = '';
  protected readonly numbers = ['1', '2', '3', '4'];
  protected isSelectingNumber: boolean = false;

  protected selectSeasonLabel = 'Выберите сезон';
  protected season: string = '';
  protected readonly seasons: string[] = ['Осень', 'Весна'];
  protected isSelectingSeason: boolean = false;

  public constructor(
    private readonly _dataService: SessionReportsDataService,
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _paginationService: SessionReportsPaginationService,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {}

  public submit(): void {
    if (this.isNumberNotNumeric()) return;
    if (this.isYearNotNumeric()) return;
    if (this.isYearNotCorrect()) return;
    const policy: IFetchPolicy<ControlWeekReportInterface[]> =
      new SessionReportPeriodFetchPolicy(
        this._authService,
        Number(this.year),
        Number(this.number),
        this.season,
        this._appConfig,
      );
    policy.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this._dataService.setPolicy(policy);
    this.filterCommited.emit();
    this.close();
  }

  protected cancel(): void {
    const policy: IFetchPolicy<ControlWeekReportInterface[]> =
      new SessionReportDefaultFetchPolicy(this._authService, this._appConfig);
    policy.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this._dataService.setPolicy(policy);
    this.filterCommited.emit();
    this.close();
  }

  protected close(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  protected handleNumberSelection(number: string): void {
    this.selectNumberLabel = number;
    this.number = number;
  }

  protected handleSeasonSelection(season: string): void {
    this.selectSeasonLabel = season;
    this.season = season;
  }

  private isNumberNotNumeric(): boolean {
    if (IsNullOrWhiteSpace(this.number)) return false;
    try {
      Number(this.number);
      return false;
    } catch {
      this._notificationService.SetMessage =
        'Номер контрольной недели должен быть числом';
      this._notificationService.failure();
      return true;
    }
  }

  private isYearNotNumeric(): boolean {
    if (IsNullOrWhiteSpace(this.year)) return false;
    try {
      Number(this.year);
      return false;
    } catch {
      this._notificationService.SetMessage = 'Год должен быть числом';
      this._notificationService.failure();
      return true;
    }
  }

  private isYearNotCorrect(): boolean {
    if (IsNullOrWhiteSpace(this.year)) return false;
    if (this.year.length > 4) {
      this._notificationService.SetMessage = `${this.year} год некорректный`;
      this._notificationService.failure();
      return true;
    }
    return false;
  }
}
