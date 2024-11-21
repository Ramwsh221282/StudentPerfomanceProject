import { Component, EventEmitter, Output } from '@angular/core';
import { SessionReportsPaginationService } from './session-reports-pagination-service';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-session-reports-pagination',
  standalone: true,
  imports: [NgClass],
  templateUrl: './session-reports-pagination.component.html',
  styleUrl: './session-reports-pagination.component.scss',
})
export class SessionReportsPaginationComponent {
  @Output() pageEmitter: EventEmitter<void> = new EventEmitter();

  public constructor(
    protected readonly service: SessionReportsPaginationService,
  ) {
    service.refreshPagination();
  }
}
