import { Component, Input, OnInit } from '@angular/core';
import { NgIf } from '@angular/common';
import { TeacherJournal } from '../../../assignments-page/models/teacher-journal';

@Component({
  selector: 'app-teacher-assignments-status',
  imports: [NgIf],
  templateUrl: './teacher-assignments-status.component.html',
  styleUrl: './teacher-assignments-status.component.scss',
  standalone: true,
})
export class TeacherAssignmentsStatusComponent implements OnInit {
  @Input({ required: true }) journalsWithRequireAssignments: TeacherJournal[] =
    [];
  public disciplinesCountThatRequireAssignments: number = 0;
  public studentsCountThatRequireAssignments: number = 0;

  public ngOnInit() {
    this.initCount();
  }

  private initCount(): void {
    for (const journal of this.journalsWithRequireAssignments) {
      for (const discipline of journal.disciplines) {
        if (discipline.requiresAssignment)
          this.disciplinesCountThatRequireAssignments++;
        for (const students of discipline.students) {
          if (students.requiresAssignment)
            this.studentsCountThatRequireAssignments++;
        }
      }
    }
  }
}
