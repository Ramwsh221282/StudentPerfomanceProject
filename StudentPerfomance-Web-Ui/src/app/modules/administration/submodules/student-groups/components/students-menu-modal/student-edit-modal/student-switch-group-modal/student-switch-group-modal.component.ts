import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { StudentGroupSearchService } from '../../../../services/student-group-search.service';
import { StudentGroup } from '../../../../services/studentsGroup.interface';
import { StudentGroupBuilder } from '../../../../models/student-group-builder';

@Component({
  selector: 'app-student-switch-group-modal',
  templateUrl: './student-switch-group-modal.component.html',
  styleUrl: './student-switch-group-modal.component.scss',
  providers: [StudentGroupSearchService],
})
export class StudentSwitchGroupModalComponent implements OnInit {
  @Input({ required: true }) initialGroup: StudentGroup;
  @Output()
  visibility: EventEmitter<boolean> = new EventEmitter();
  @Output() selectedGroup: EventEmitter<StudentGroup> = new EventEmitter();

  protected group: StudentGroup;
  protected groups: StudentGroup[];

  public constructor(private readonly _service: StudentGroupSearchService) {
    this.group = {} as StudentGroup;
    this.groups = [];
  }

  public ngOnInit(): void {
    const builder = new StudentGroupBuilder();
    this.group = { ...builder.buildDefault() };
    this.fetchAllGroups();
  }

  protected fetchAllGroups(): void {
    this._service.getAllGroups().subscribe((response) => {
      this.groups = response;
    });
  }

  protected searchGroups(): void {
    this._service.searchGroups(this.group).subscribe((response) => {
      this.groups = response;
    });
  }

  protected selectGroup(group: StudentGroup): void {
    this.group = { ...group };
  }

  protected close(): void {
    if (this.initialGroup.name != this.group.name)
      this.selectedGroup.emit(this.group);
    this.visibility.emit(false);
  }
}
