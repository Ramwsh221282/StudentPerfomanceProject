import { Component, Input, OnInit } from '@angular/core';
import { SemesterPlanFacadeService } from '../../services/semester-plan-facade.service';
import { SemesterPlan } from '../../models/semester-plan.interface';

@Component({
  selector: 'app-plan-table',
  templateUrl: './plan-table.component.html',
  styleUrl: './plan-table.component.scss',
})
export class PlanTableComponent implements OnInit {
  @Input({ required: true }) public semesterPlans: SemesterPlan[];

  public constructor(
    protected readonly facadeService: SemesterPlanFacadeService
  ) {}

  public ngOnInit(): void {
    this.facadeService.fetch();
  }
}
