import { Component, OnInit } from '@angular/core';
import { SemesterPlanFacadeService } from '../../../services/semester-plan-facade.service';

@Component({
  selector: 'app-plan-pagination',
  templateUrl: './plan-pagination.component.html',
  styleUrl: './plan-pagination.component.scss',
})
export class PlanPaginationComponent implements OnInit {
  public constructor(
    protected readonly facadeService: SemesterPlanFacadeService
  ) {}

  public ngOnInit(): void {
    this.facadeService.refreshPagination();
  }
}
