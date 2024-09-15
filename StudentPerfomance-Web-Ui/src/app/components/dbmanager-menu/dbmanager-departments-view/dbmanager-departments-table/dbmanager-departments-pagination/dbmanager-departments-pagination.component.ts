import { Component, OnInit } from '@angular/core';
import { NgClass } from '@angular/common';
import { DepartmentsFacadeService } from '../../services/departments-facade.service';

@Component({
  selector: 'app-dbmanager-departments-pagination',
  standalone: true,
  imports: [NgClass],
  templateUrl: './dbmanager-departments-pagination.component.html',
  styleUrl: './dbmanager-departments-pagination.component.scss',
})
export class DbmanagerDepartmentsPaginationComponent implements OnInit {
  public constructor(
    protected readonly facadeService: DepartmentsFacadeService
  ) {}

  public ngOnInit(): void {
    this.facadeService.refreshPagination();
  }
}
