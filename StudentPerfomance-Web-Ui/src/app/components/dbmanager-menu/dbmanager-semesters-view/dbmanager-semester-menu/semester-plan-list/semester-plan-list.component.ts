import { Component, Input, OnInit } from '@angular/core';
import { SemesterPlanCardComponent } from './semester-plan-card/semester-plan-card.component';
import { SemesterPlanPaginationComponent } from './semester-plan-pagination/semester-plan-pagination.component';
import { SemesterPlan } from '../models/semester-plan.interface';
import { SemesterPlanFacadeService } from '../services/semester-plan-facade.service';

@Component({
  selector: 'app-semester-plan-list',
  standalone: true,
  imports: [SemesterPlanCardComponent, SemesterPlanPaginationComponent],
  templateUrl: './semester-plan-list.component.html',
  styleUrl: './semester-plan-list.component.scss',
})
export class SemesterPlanListComponent implements OnInit {
  @Input({ required: true }) public semesterPlans: SemesterPlan[];
  public constructor(
    protected readonly facadeService: SemesterPlanFacadeService
  ) {}

  public ngOnInit(): void {
    this.facadeService.fetch();
  }
}
