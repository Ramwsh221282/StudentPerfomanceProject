import { Component, OnInit } from '@angular/core';
import { DepartmentsFacadeService } from '../../../services/departments-facade.service';

@Component({
  selector: 'app-pagination-department',
  templateUrl: './pagination-department.component.html',
  styleUrl: './pagination-department.component.scss',
})
export class PaginationDepartmentComponent implements OnInit {
  public constructor(
    protected readonly facadeService: DepartmentsFacadeService
  ) {}

  public ngOnInit(): void {
    this.facadeService.refreshPagination();
  }
}
