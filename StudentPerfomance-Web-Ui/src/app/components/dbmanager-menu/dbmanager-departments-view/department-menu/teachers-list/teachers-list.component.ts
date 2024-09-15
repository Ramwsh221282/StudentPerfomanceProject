import { Component, OnInit } from '@angular/core';
import { TeachersPaginationComponent } from './teachers-pagination/teachers-pagination.component';
import { FacadeTeacherService } from '../services/facade-teacher.service';

@Component({
  selector: 'app-teachers-list',
  standalone: true,
  imports: [TeachersPaginationComponent],
  templateUrl: './teachers-list.component.html',
  styleUrl: './teachers-list.component.scss',
})
export class TeachersListComponent implements OnInit {
  public constructor(protected readonly facadeService: FacadeTeacherService) {}
  public ngOnInit(): void {
    this.facadeService.fetch();
  }
}
