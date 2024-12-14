import { Component, Input } from '@angular/core';
import { GroupReportInterface } from '../../../../Models/Data/group-report-interface';
import { DatePipe, NgClass, NgIf } from '@angular/common';
import { StudentReportInterface } from '../../../../Models/Data/student-report-interface';
import { DisciplineReportInterface } from '../../../../Models/Data/discipline-report-interface';
import { toPng } from 'html-to-image';
import jsPDF from 'jspdf';

@Component({
  selector: 'app-session-group-report-content',
  standalone: true,
  imports: [NgIf, DatePipe, NgClass],
  templateUrl: './session-group-report-content.component.html',
  styleUrl: './session-group-report-content.component.scss',
})
export class SessionGroupReportContentComponent {
  @Input({ required: true }) currentGroup: GroupReportInterface;
  @Input({ required: true }) students: StudentReportInterface[];
  @Input({ required: true }) disciplines: DisciplineReportInterface[];
  @Input({ required: true }) season: string;
  @Input({ required: true }) number: number;
  @Input({ required: true }) startDate: string;
  @Input({ required: true }) endDate: string;

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

  protected isBadMark(grade: string): boolean {
    return grade === '2' || grade === 'НП' || grade === 'НА';
  }

  protected saveAsPdf(): void {
    const report = document.getElementById('report') as HTMLElement;
    const scale = 3;
    const width = report.offsetWidth * scale;
    const height = report.offsetHeight * scale;

    toPng(report, { pixelRatio: scale }).then((dataUrl) => {
      var img = new Image();
      img.src = dataUrl;

      const pageWidth = 210;
      const pageHeight = (height * pageWidth) / width;

      img.onload = () => {
        const pdf = new jsPDF('p', 'mm', 'a4');
        pdf.addImage(img, 'PNG', 0, 0, pageWidth, pageHeight);
        pdf.save(`${this.currentGroup.groupName}_ОТЧЁТ.pdf`);
      };
    });
  }
}
