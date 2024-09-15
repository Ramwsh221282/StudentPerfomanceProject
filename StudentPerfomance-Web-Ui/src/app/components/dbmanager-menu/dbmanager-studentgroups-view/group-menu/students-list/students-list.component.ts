import { Component, OnInit } from '@angular/core';
import { StudentsPaginationComponent } from './students-pagination/students-pagination.component';
import { FacadeStudentService } from '../services/facade-student.service';

@Component({
  selector: 'app-students-list',
  standalone: true,
  imports: [StudentsPaginationComponent],
  templateUrl: './students-list.component.html',
  styleUrl: './students-list.component.scss',
})
export class StudentsListComponent implements OnInit {
  public constructor(protected readonly facadeService: FacadeStudentService) {}

  public ngOnInit(): void {
    this.facadeService.fetchData();
    this.facadeService.refreshPagination();
  }
}
