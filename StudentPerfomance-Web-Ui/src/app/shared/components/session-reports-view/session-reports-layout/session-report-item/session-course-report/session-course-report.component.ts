import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { SessionReportsDataService } from '../../../Services/data-services/session-reports-data-service';
import { CourseReportInterface } from '../../../Models/Data/course-report-interface';
import { DirectionCodeReportInterface } from '../../../Models/Data/direction-code-report-interface';
import { DirectionTypeReportInterface } from '../../../Models/Data/direction-type-report-interface';
import { DatePipe } from '@angular/common';
import { GroupReportInterface } from '../../../Models/Data/group-report-interface';
import { toPng } from 'html-to-image';
import jsPDF from 'jspdf';

@Component({
  selector: 'app-session-course-report',
  standalone: true,
  imports: [DatePipe, RouterLink],
  templateUrl: './session-course-report.component.html',
  styleUrl: './session-course-report.component.scss',
})
export class SessionCourseReportComponent implements OnInit {
  protected creationDate: string = '';
  protected completionDate: string = '';
  protected courseReports: CourseReportInterface[] = [];
  protected directionCodeReports: DirectionCodeReportInterface[] = [];
  protected directionTypeReports: DirectionTypeReportInterface[] = [];
  protected groupReports: GroupReportInterface[];
  protected average: number;
  protected perfomance: number;

  public constructor(
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _dataService: SessionReportsDataService,
  ) {}

  public ngOnInit(): void {
    this._activatedRoute.queryParams.subscribe((params) => {
      const id = params['reportId'];
      this.creationDate = params['startDate'];
      this.completionDate = params['endDate'];
      this.initializeReports(id);
    });
  }

  private initializeReports(id: string): void {
    this._dataService.getCourseReportById(id).subscribe((response) => {
      this.courseReports = response.courseParts;
      this.directionCodeReports = response.directionCodeReport;
      this.directionTypeReports = response.directionTypeReport;
      this.groupReports = response.groupParts;
      this.average = response.average;
      this.perfomance = response.perfomance;
    });
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
        pdf.save(`${this.creationDate}_${this.completionDate}_ОТЧЁТ.pdf`);
      };
    });
  }
}
