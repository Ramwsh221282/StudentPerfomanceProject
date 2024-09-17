import { Component, OnInit } from '@angular/core';
import { FacadeStudentService } from '../../../services/facade-student.service';

@Component({
  selector: 'app-student-pagination',
  templateUrl: './student-pagination.component.html',
  styleUrl: './student-pagination.component.scss',
})
export class StudentPaginationComponent implements OnInit {
  public constructor(protected readonly facadeService: FacadeStudentService) {}

  public ngOnInit(): void {
    this.facadeService.refreshPagination();
  }
}
