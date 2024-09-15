import { Component, OnInit } from '@angular/core';
import { DbmanagerDepartmentsPaginationComponent } from './dbmanager-departments-pagination/dbmanager-departments-pagination.component';
import { RouterLink } from '@angular/router';
import { DepartmentsFacadeService } from '../services/departments-facade.service';

@Component({
  selector: 'app-dbmanager-departments-table',
  standalone: true,
  imports: [DbmanagerDepartmentsPaginationComponent, RouterLink],
  templateUrl: './dbmanager-departments-table.component.html',
  styleUrl: './dbmanager-departments-table.component.scss',
})
export class DbmanagerDepartmentsTableComponent implements OnInit {
  public constructor(
    protected readonly facadeService: DepartmentsFacadeService
  ) {}

  public ngOnInit(): void {
    this.facadeService.fetchData();
  }
}
