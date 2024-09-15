import { Component } from '@angular/core';
import { NgClass } from '@angular/common';
import { SemesterPlanFacadeService } from '../../services/semester-plan-facade.service';

@Component({
  selector: 'app-semester-plan-pagination',
  standalone: true,
  imports: [NgClass],
  templateUrl: './semester-plan-pagination.component.html',
  styleUrl: './semester-plan-pagination.component.scss',
})
export class SemesterPlanPaginationComponent {
  public constructor(
    protected readonly facadeService: SemesterPlanFacadeService
  ) {}
}
