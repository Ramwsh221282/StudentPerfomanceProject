import { Component, Input } from '@angular/core';
import { GroupReportInterface } from '../../../../Models/Data/group-report-interface';
import { NgIf } from '@angular/common';
import { StudentReportInterface } from '../../../../Models/Data/student-report-interface';
import { DisciplineReportInterface } from '../../../../Models/Data/discipline-report-interface';

@Component({
  selector: 'app-session-group-report-content',
  standalone: true,
  imports: [NgIf],
  templateUrl: './session-group-report-content.component.html',
  styleUrl: './session-group-report-content.component.scss',
})
export class SessionGroupReportContentComponent {
  @Input({ required: true }) currentGroup: GroupReportInterface;
  @Input({ required: true }) students: StudentReportInterface[];
  @Input({ required: true }) disciplines: DisciplineReportInterface[];

  protected getStudentGrade(
    disciplineId: string,
    studentRecordbook: number,
  ): string {
    const discipline: DisciplineReportInterface = this.disciplines.find(
      (a) => a.id == disciplineId,
    )!;
    const grade = discipline.parts.find(
      (s) => s.recordbook == studentRecordbook,
    )!;
    return grade.grade;
  }
}
