import { Component, OnInit } from '@angular/core';
import { NgClass } from '@angular/common';
import { FacadeTeacherService } from '../../services/facade-teacher.service';

@Component({
  selector: 'app-teachers-pagination',
  standalone: true,
  imports: [NgClass],
  templateUrl: './teachers-pagination.component.html',
  styleUrl: './teachers-pagination.component.scss',
})
export class TeachersPaginationComponent implements OnInit {
  public constructor(protected readonly facadeService: FacadeTeacherService) {}

  public ngOnInit(): void {
    this.facadeService.refreshPagination();
  }
}
