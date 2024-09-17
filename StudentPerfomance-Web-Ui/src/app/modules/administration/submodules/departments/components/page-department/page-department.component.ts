import { Component } from '@angular/core';
import { DepartmentsFacadeService } from '../../services/departments-facade.service';

@Component({
  selector: 'app-page-department',
  templateUrl: './page-department.component.html',
  styleUrl: './page-department.component.scss',
  providers: [DepartmentsFacadeService],
})
export class PageDepartmentComponent {}
