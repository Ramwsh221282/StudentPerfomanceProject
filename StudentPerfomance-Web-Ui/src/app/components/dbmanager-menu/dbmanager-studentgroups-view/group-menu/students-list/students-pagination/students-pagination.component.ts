import { Component } from '@angular/core';
import { NgClass } from '@angular/common';
import { FacadeStudentService } from '../../services/facade-student.service';

@Component({
  selector: 'app-students-pagination',
  standalone: true,
  imports: [NgClass],
  templateUrl: './students-pagination.component.html',
  styleUrl: './students-pagination.component.scss',
})
export class StudentsPaginationComponent {
  public constructor(protected readonly facadeService: FacadeStudentService) {}
}
