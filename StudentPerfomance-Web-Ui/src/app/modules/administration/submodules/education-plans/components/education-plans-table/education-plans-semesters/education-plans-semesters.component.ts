import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EducationPlanSemestersService } from './education-plans-semesters-data.service';
import { Semester } from '../../../../semesters/models/semester.interface';
import { EducationPlan } from '../../../models/education-plan-interface';

@Component({
  selector: 'app-education-plans-semesters',
  templateUrl: './education-plans-semesters.component.html',
  styleUrl: './education-plans-semesters.component.scss',
  providers: [EducationPlanSemestersService],
})
export class EducationPlansSemestersComponent implements OnInit {
  @Output() visibility: EventEmitter<boolean> = new EventEmitter();
  @Input({ required: true }) plan: EducationPlan;

  protected semesters: Semester[];
  protected activeSemester: Semester;
  protected activeSemesterModalVisibility: boolean;

  public constructor(private readonly _service: EducationPlanSemestersService) {
    this.semesters = [];
    this.activeSemester = {} as Semester;
    this.activeSemesterModalVisibility = false;
  }

  public ngOnInit(): void {
    this.refreshSemestersTable();
  }

  protected close(): void {
    this.visibility.emit(false);
  }

  protected openSemesterModal(semester: Semester): void {
    this.activeSemester = semester;
    this.activeSemesterModalVisibility = true;
  }

  protected closeSemesterModal(value: boolean): void {
    this.activeSemester = {} as Semester;
    this.activeSemesterModalVisibility = false;
    this.refreshSemestersTable();
  }

  private refreshSemestersTable(): void {
    this._service.getPlanSemesters(this.plan).subscribe((response) => {
      this.semesters = response;
    });
  }
}
