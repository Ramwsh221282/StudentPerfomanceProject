import { Component, Input, OnInit } from '@angular/core';
import { AssignmentSession } from '../../../../../modules/administration/submodules/assignment-sessions/models/assignment-session-interface';
import { AssignmentSessionWeek } from '../../../../../modules/administration/submodules/assignment-sessions/models/assignment-session-week';
import { NgClass, NgForOf } from '@angular/common';
import { ControlWeekGroupItemComponent } from './control-week-group-item/control-week-group-item.component';

@Component({
  selector: 'app-control-week-groups-part',
  imports: [NgForOf, NgClass, ControlWeekGroupItemComponent],
  templateUrl: './control-week-groups-part.component.html',
  styleUrl: './control-week-groups-part.component.scss',
  standalone: true,
})
export class ControlWeekGroupsPartComponent implements OnInit {
  @Input({ required: true }) session: AssignmentSession;
  public groupTabs: string[] = [];
  public currentTab: string;

  public ngOnInit() {
    for (const week of this.session.weeks) {
      this.groupTabs.push(week.groupName.name);
    }
    this.currentTab = this.groupTabs[0];
  }

  public resolveSelectedGroupTab(): AssignmentSessionWeek {
    return this.session.weeks.find(
      (week) => week.groupName.name == this.currentTab,
    )!;
  }
}
