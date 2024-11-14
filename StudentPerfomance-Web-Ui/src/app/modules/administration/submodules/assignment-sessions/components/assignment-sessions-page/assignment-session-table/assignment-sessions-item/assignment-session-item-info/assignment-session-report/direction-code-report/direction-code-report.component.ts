import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DirectionCodeReportEntity } from '../../../../../../../models/report/direction-code-report-entity';

@Component({
  selector: 'app-direction-code-report',
  templateUrl: './direction-code-report.component.html',
  styleUrl: './direction-code-report.component.scss',
})
export class DirectionCodeReportComponent {
  @Input({ required: true }) report: DirectionCodeReportEntity;
  @Output() visibility: EventEmitter<void> = new EventEmitter();
}
