import { Component, Input, OnInit } from '@angular/core';
import { AssignmentSessionWeek } from '../../../../models/assignment-session-week';

@Component({
  selector: 'app-session-item-group-assignments',
  templateUrl: './session-item-group-assignments.component.html',
  styleUrl: './session-item-group-assignments.component.scss',
})
export class SessionItemGroupAssignmentsComponent implements OnInit {
  @Input({ required: true }) weeks: AssignmentSessionWeek[];
  protected tabs: any = [];

  public ngOnInit(): void {
    for (const week of this.weeks) {
      this.tabs.push({ group: week.groupName });
    }
  }

  protected currentTabName: string = '';

  protected resolveSelectedAssignmentSessionWeek(): AssignmentSessionWeek {
    return this.weeks.find(
      (week) => week.groupName.name == this.currentTabName,
    )!;
  }
}
