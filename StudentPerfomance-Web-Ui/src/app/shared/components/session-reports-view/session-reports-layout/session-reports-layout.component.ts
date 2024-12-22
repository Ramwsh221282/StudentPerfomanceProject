import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SessionReportItemComponent } from './session-report-item/session-report-item.component';
import { ControlWeekReportInterface } from '../Models/Data/control-week-report-interface';
import { BlueButtonComponent } from '../../../../building-blocks/buttons/blue-button/blue-button.component';
import { NgForOf, NgIf } from '@angular/common';
import { SessionReportsFilterDropdownComponent } from './session-reports-filter-dropdown/session-reports-filter-dropdown.component';
import { SessionReportItemRemoveModalComponent } from './session-report-item/session-report-item-remove-modal/session-report-item-remove-modal.component';

@Component({
  selector: 'app-session-reports-layout',
  standalone: true,
  imports: [
    SessionReportItemComponent,
    BlueButtonComponent,
    NgForOf,
    SessionReportsFilterDropdownComponent,
    NgIf,
    SessionReportItemRemoveModalComponent,
  ],
  templateUrl: './session-reports-layout.component.html',
  styleUrl: './session-reports-layout.component.scss',
})
export class SessionReportsLayoutComponent {
  @Input({ required: true }) reports: ControlWeekReportInterface[];
  @Output() removeAccepted: EventEmitter<ControlWeekReportInterface> =
    new EventEmitter();
  @Output() filterAccepted: EventEmitter<void> = new EventEmitter();

  protected reportToRemove: ControlWeekReportInterface | null;
  protected isRemovingReport: boolean = false;

  protected isFiltering: boolean = false;
}
