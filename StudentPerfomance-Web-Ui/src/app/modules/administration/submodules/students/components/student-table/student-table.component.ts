import { Component, OnInit } from '@angular/core';
import { FacadeStudentService } from '../../services/facade-student.service';

@Component({
  selector: 'app-student-table',
  templateUrl: './student-table.component.html',
  styleUrl: './student-table.component.scss',
})
export class StudentTableComponent implements OnInit {
  public constructor(protected readonly facadeService: FacadeStudentService) {}

  public ngOnInit(): void {
    this.facadeService.fetchData();
  }
}
