import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ControlWeekReportInterface } from '../../Models/Data/control-week-report-interface';
import { DatePipe, NgIf } from '@angular/common';
import { RouterLink } from '@angular/router';
import { SessionReportItemRemoveModalComponent } from './session-report-item-remove-modal/session-report-item-remove-modal.component';
import { AuthService } from '../../../../../modules/users/services/auth.service';

@Component({
  selector: 'app-session-report-item',
  standalone: true,
  imports: [DatePipe, RouterLink, SessionReportItemRemoveModalComponent, NgIf],
  templateUrl: './session-report-item.component.html',
  styleUrl: './session-report-item.component.scss',
})
export class SessionReportItemComponent {
  @Input({ required: true }) report: ControlWeekReportInterface;
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();

  protected isRemovingVisible: boolean = false;

  public constructor(
    protected readonly date: DatePipe,
    protected readonly _authService: AuthService,
  ) {}
}
