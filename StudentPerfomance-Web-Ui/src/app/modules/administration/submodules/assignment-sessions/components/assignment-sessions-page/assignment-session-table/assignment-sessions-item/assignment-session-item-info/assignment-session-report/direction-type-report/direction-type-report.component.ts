import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DirectionTypeReportEntity } from '../../../../../../../models/report/direction-type-report-entity';

@Component({
  selector: 'app-direction-type-report',
  templateUrl: './direction-type-report.component.html',
  styleUrl: './direction-type-report.component.scss',
})
export class DirectionTypeReportComponent {
  @Input({ required: true }) report: DirectionTypeReportEntity;
  @Output() visibility: EventEmitter<void> = new EventEmitter();
}
