import { Component, OnInit } from '@angular/core';
import { StudentGroupsCreateDataService } from '../../services/student-groups-create-data.service';
import { StudentGroupsDeleteDataService } from '../../services/student-groups-delete-data.service';
import { StudentGroupsFetchDataService } from '../../services/student-groups-fetch-data.service';
import { StudentGroupsMergeDataService } from '../../services/student-groups-merge-data.service';
import { StudentGroupsPaginationService } from '../../services/student-groups-pagination.service';
import { StudentGroupsUpdateDataService } from '../../services/student-groups-update-data.service';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { Student } from '../../../students/models/student.interface';

@Component({
  selector: 'app-student-groups-page',
  templateUrl: './student-groups-page.component.html',
  styleUrl: './student-groups-page.component.scss',
  providers: [
    StudentGroupsCreateDataService,
    StudentGroupsDeleteDataService,
    StudentGroupsFetchDataService,
    StudentGroupsMergeDataService,
    StudentGroupsPaginationService,
    StudentGroupsUpdateDataService,
    StudentGroupsFacadeService,
    UserOperationNotificationService,
  ],
})
export class StudentGroupsPageComponent implements OnInit {
  protected groups: StudentGroup[] = [];
  protected selectedGroup: StudentGroup | null;

  public constructor(
    protected readonly facadeService: StudentGroupsFacadeService,
    protected readonly notificationService: UserOperationNotificationService,
  ) {}

  public ngOnInit(): void {
    this.facadeService.fetchData().subscribe((response) => {
      this.groups = response;
    });
  }

  protected handleStudentGroupSwitch(student: Student): void {
    this.excludeStudentFromPreviousGroup(student);
    this.appendStudentInNewGroup(student);
    this.notificationService.SetMessage = `Студент ${student.surname} ${student.name[0]} ${student.patronymic == null ? '' : student.patronymic[0]} переведён в группу ${student.group.name}`;
    this.notificationService.success();
  }

  private excludeStudentFromPreviousGroup(updatedStudent: Student): void {
    for (const group of this.groups) {
      for (const student of group.students) {
        if (student.recordbook === updatedStudent.recordbook) {
          const index = group.students.indexOf(student);
          if (index !== -1) {
            group.students.splice(index, 1);
          }
        }
      }
    }
  }

  private appendStudentInNewGroup(updatedStudent: Student) {
    for (const group of this.groups) {
      if (group.name == updatedStudent.group.name) {
        group.students.push(updatedStudent);
        group.students.sort((a, b) =>
          a.surname > b.surname ? 1 : a.surname < b.surname ? -1 : 0,
        );
      }
    }
  }
}
