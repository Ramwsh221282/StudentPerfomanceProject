import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../services/studentsGroup.interface';

@Component({
  selector: 'app-student-groups-table',
  templateUrl: './student-groups-table.component.html',
  styleUrl: './student-groups-table.component.scss',
})
export class StudentGroupsTableComponent {
  @Input({ required: true }) groups: StudentGroup[];
  @Output() studentGroupSelected: EventEmitter<StudentGroup> =
    new EventEmitter();
  protected currentlySelectedGroup: StudentGroup | null;

  protected handleGroupSelection(group: StudentGroup): boolean {
    if (!this.currentlySelectedGroup) return false;
    return group.name == this.currentlySelectedGroup.name;
  }
}
