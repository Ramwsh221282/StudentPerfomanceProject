import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-student-groups-table',
  templateUrl: './student-groups-table.component.html',
  styleUrl: './student-groups-table.component.scss',
})
export class StudentGroupsTableComponent {
  @Input({ required: true }) groups: StudentGroup[];
  @Output() studentGroupSelected: EventEmitter<StudentGroup> =
    new EventEmitter();
  @Output() filtered: EventEmitter<void> = new EventEmitter();
  @Output() groupDeleted: EventEmitter<void> = new EventEmitter();
  @Output() groupMerged: EventEmitter<void> = new EventEmitter();
  @Output() groupCreated: EventEmitter<void> = new EventEmitter();
  @Output() pageChanged: EventEmitter<void> = new EventEmitter();

  protected currentlySelectedGroup: StudentGroup | null;

  protected isCreatingGroup: boolean = false;
  protected isFilteringGroups: boolean = false;

  protected isRemovingGroup: boolean = false;
  protected groupToRemove: StudentGroup | null;

  protected isMergingGroups: boolean = false;

  public constructor(private readonly router: Router) {}

  protected handleGroupSelection(group: StudentGroup): boolean {
    if (!this.currentlySelectedGroup) return false;
    return group.name == this.currentlySelectedGroup.name;
  }

  protected handleGroupCreation(group: StudentGroup): void {
    this.groupCreated.emit();
  }

  protected moveToDocumentation(): void {
    const route = ['/groups-info'];
    this.router.navigate(route);
  }

  protected handleGroupMerge(): void {
    this.currentlySelectedGroup = null;
    this.groupMerged.emit();
  }

  protected handlePageChange(): void {
    this.pageChanged.emit();
  }
}
