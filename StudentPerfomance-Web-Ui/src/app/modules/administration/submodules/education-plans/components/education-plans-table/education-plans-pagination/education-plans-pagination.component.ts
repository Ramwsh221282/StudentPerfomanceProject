import { Component, OnInit } from '@angular/core';
import { FacadeService } from '../../../services/facade.service';

@Component({
  selector: 'app-education-plans-pagination',
  templateUrl: './education-plans-pagination.component.html',
  styleUrl: './education-plans-pagination.component.scss',
})
export class EducationPlansPaginationComponent implements OnInit {
  public constructor(protected readonly facadeService: FacadeService) {}

  ngOnInit(): void {
    this.facadeService.refreshPagination();
  }
}
