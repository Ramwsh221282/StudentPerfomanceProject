import { Component, OnInit } from '@angular/core';
import { DepartmentsFacadeService } from '../../services/departments-facade.service';

@Component({
  selector: 'app-table-department',
  templateUrl: './table-department.component.html',
  styleUrl: './table-department.component.scss',
})
export class TableDepartmentComponent implements OnInit {
  public constructor(
    protected readonly facadeService: DepartmentsFacadeService
  ) {}

  public ngOnInit(): void {
    this.facadeService.fetchData();
  }
}
