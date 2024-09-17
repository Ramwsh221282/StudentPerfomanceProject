import { Component, OnInit } from '@angular/core';
import { FacadeTeacherService } from '../../services/facade-teacher.service';

@Component({
  selector: 'app-teacher-table',
  templateUrl: './teacher-table.component.html',
  styleUrl: './teacher-table.component.scss',
})
export class TeacherTableComponent implements OnInit {
  public constructor(protected readonly facadeService: FacadeTeacherService) {}

  public ngOnInit(): void {
    this.facadeService.fetch();
  }
}
