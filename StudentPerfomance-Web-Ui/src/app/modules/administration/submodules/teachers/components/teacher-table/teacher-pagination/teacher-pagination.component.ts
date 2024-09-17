import { Component, OnInit } from '@angular/core';
import { FacadeTeacherService } from '../../../services/facade-teacher.service';

@Component({
  selector: 'app-teacher-pagination',
  templateUrl: './teacher-pagination.component.html',
  styleUrl: './teacher-pagination.component.scss',
})
export class TeacherPaginationComponent implements OnInit {
  public constructor(protected readonly facadeService: FacadeTeacherService) {}

  public ngOnInit(): void {
    this.facadeService.refreshPagination();
  }
}
