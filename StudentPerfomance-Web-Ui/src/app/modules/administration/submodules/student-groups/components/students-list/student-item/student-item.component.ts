import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Student } from '../../../../students/models/student.interface';
import { StudentGroup } from '../../../services/studentsGroup.interface';

@Component({
    selector: 'app-student-item',
    templateUrl: './student-item.component.html',
    styleUrl: './student-item.component.scss',
    standalone: false
})
export class StudentItemComponent implements OnInit {
  @Input({ required: true }) student: Student;
  @Input({ required: true }) group: StudentGroup;
  @Output() editingStudent: EventEmitter<Student> = new EventEmitter();
  @Output() movingStudentToOtherGroup: EventEmitter<Student> =
    new EventEmitter();
  @Output() removingStudent: EventEmitter<Student> = new EventEmitter();

  public ngOnInit(): void {
    this.student.group = { ...this.group };
  }
}
