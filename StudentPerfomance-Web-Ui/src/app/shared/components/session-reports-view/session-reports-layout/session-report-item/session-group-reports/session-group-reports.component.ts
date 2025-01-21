import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { SessionReportsDataService } from '../../../Services/data-services/session-reports-data-service';
import { DatePipe, NgIf } from '@angular/common';
import { SessionGroupReportControlBarComponent } from './session-group-report-control-bar/session-group-report-control-bar.component';
import { SessionGroupReportContentComponent } from './session-group-report-content/session-group-report-content.component';
import { GroupReportInterface } from '../../../Models/Data/group-report-interface';
import { DisciplineReportInterface } from '../../../Models/Data/discipline-report-interface';
import { StudentReportInterface } from '../../../Models/Data/student-report-interface';

@Component({
    selector: 'app-session-group-reports',
    imports: [
        DatePipe,
        RouterLink,
        SessionGroupReportControlBarComponent,
        SessionGroupReportContentComponent,
        NgIf,
    ],
    templateUrl: './session-group-reports.component.html',
    styleUrl: './session-group-reports.component.scss'
})
export class SessionGroupReportsComponent implements OnInit {
  protected isInited: boolean = false;
  protected groups: GroupReportInterface[];
  protected startDate: string = '';
  protected endDate: string = '';
  protected season: string = '';
  protected number: number = 0;

  protected currentGroup: GroupReportInterface;
  protected disciplines: DisciplineReportInterface[];
  protected students: StudentReportInterface[];

  public constructor(
    private readonly route: ActivatedRoute,
    private readonly dataService: SessionReportsDataService,
  ) {}

  public ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      console.log(params);
      const reportId: string = params['reportId'];
      this.startDate = params['startDate'];
      this.endDate = params['endDate'];
      this.season = params['season'];
      this.number = params['number'];
      this.initializeReport(reportId);
    });
  }

  private initializeReport(id: string): void {
    this.dataService.getById(id).subscribe((response) => {
      this.groups = response;
      this.currentGroup = this.groups[0];
      this.initializeDisciplinesFromGroup(this.currentGroup);
      this.initializeStudentsFromGroup(this.currentGroup);
      this.isInited = true;
    });
  }

  protected initializeStudentsFromGroup(group: GroupReportInterface): void {
    if (group.parts[0] == null) return;
    this.students = [];
    for (const student of group.parts[0].parts) {
      this.students.push(student);
    }
  }

  protected initializeDisciplinesFromGroup(group: GroupReportInterface): void {
    if (group.parts == null) return;
    this.disciplines = [];
    for (const discipline of group.parts) {
      this.disciplines.push(discipline);
    }
  }
}
