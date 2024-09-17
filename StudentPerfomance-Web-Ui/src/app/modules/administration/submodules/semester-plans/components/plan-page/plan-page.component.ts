import { Component, OnInit } from '@angular/core';
import { SemesterPlanFacadeService } from '../../services/semester-plan-facade.service';
import { ActivatedRoute } from '@angular/router';
import { Semester } from '../../../semesters/models/semester.interface';

@Component({
  selector: 'app-plan-page',
  templateUrl: './plan-page.component.html',
  styleUrl: './plan-page.component.scss',
})
export class PlanPageComponent implements OnInit {
  public constructor(
    protected readonly facadeService: SemesterPlanFacadeService,
    private readonly _activatedRoute: ActivatedRoute
  ) {}

  public ngOnInit(): void {
    this._activatedRoute.params.subscribe((params) => {
      const currentSemester: Semester = {
        number: params['number'],
        groupName: params['groupName'],
        contractsCount: params['contractsCount'],
      } as Semester;
      this.facadeService.initialize(currentSemester);
    });
  }
}
