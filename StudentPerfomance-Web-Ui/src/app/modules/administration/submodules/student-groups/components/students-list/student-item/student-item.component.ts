import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Student } from '../../../../students/models/student.interface';

@Component({
  selector: 'app-student-item',
  templateUrl: './student-item.component.html',
  styleUrl: './student-item.component.scss',
})
export class StudentItemComponent {
  @Input({ required: true }) student: Student;
  @Output() editingStudent: EventEmitter<Student> = new EventEmitter();
}
