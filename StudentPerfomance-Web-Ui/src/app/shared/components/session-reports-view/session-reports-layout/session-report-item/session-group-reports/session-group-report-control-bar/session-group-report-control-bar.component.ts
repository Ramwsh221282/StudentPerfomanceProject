import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GroupReportInterface } from '../../../../Models/Data/group-report-interface';
import { NgIf } from '@angular/common';
import { SessionGroupReportContentComponent } from '../session-group-report-content/session-group-report-content.component';

@Component({
  selector: 'app-session-group-report-control-bar',
  standalone: true,
  imports: [NgIf, SessionGroupReportContentComponent],
  templateUrl: './session-group-report-control-bar.component.html',
  styleUrl: './session-group-report-control-bar.component.scss',
})
export class SessionGroupReportControlBarComponent {
  @Input({ required: true }) groups: GroupReportInterface[];
  @Output() selectedGroupEmitter: EventEmitter<GroupReportInterface> =
    new EventEmitter();
}
