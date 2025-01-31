import { Component, OnInit } from '@angular/core';
import { TeacherAssignmentsDataService } from './teacher-assignments-data.service';
import { TeacherAssignmentsStatusComponent } from './teacher-assignments-status/teacher-assignments-status.component';
import { NgIf } from '@angular/common';
import { TeacherJournalNavigationBlockComponent } from './teacher-journal-navigation-block/teacher-journal-navigation-block.component';
import { TeacherAssignmentInfo } from '../../assignments-page/models/teacher-assignment-info';
import { TeacherJournal } from '../../assignments-page/models/teacher-journal';

@Component({
  selector: 'app-teacher-user-short-info',
  imports: [
    TeacherAssignmentsStatusComponent,
    NgIf,
    TeacherJournalNavigationBlockComponent,
  ],
  templateUrl: './teacher-user-short-info.component.html',
  styleUrl: './teacher-user-short-info.component.scss',
  standalone: true,
})
export class TeacherUserShortInfoComponent implements OnInit {
  public teacherAssignmentsInfo: TeacherAssignmentInfo | null = null;
  public journalsWithRequireAssignments: TeacherJournal[] = [];

  public constructor(
    private readonly _service: TeacherAssignmentsDataService,
  ) {}

  public ngOnInit() {
    this._service.getTeacherAssignmentsInfo().subscribe((response) => {
      this.teacherAssignmentsInfo = response;
      this.initializeAssignmentRequireForStudents();
      this.initializeAssignmentRequireForGroups();
    });
  }

  public initializeAssignmentRequireForStudents(): void {
    if (this.teacherAssignmentsInfo == null) return;
    for (const journal of this.teacherAssignmentsInfo.journals) {
      journal.requiresAssignment = false;
      for (const discipline of journal.disciplines) {
        discipline.requiresAssignment = false;
        for (const student of discipline.students) {
          student.requiresAssignment = student.assignment.value == 1;
        }
      }
    }
  }

  public initializeAssignmentRequireForGroups(): void {
    if (this.teacherAssignmentsInfo == null) return;
    for (const journal of this.teacherAssignmentsInfo.journals) {
      for (const discipline of journal.disciplines) {
        for (const student of discipline.students) {
          if (student.assignment.value == 1) {
            journal.requiresAssignment = true;
            discipline.requiresAssignment = true;
            this.journalsWithRequireAssignments.push(journal);
            break;
          }
        }
      }
    }
  }
}
