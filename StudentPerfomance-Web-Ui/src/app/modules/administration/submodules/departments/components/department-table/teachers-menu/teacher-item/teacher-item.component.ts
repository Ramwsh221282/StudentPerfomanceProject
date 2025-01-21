import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Teacher } from '../../../../../teachers/models/teacher.interface';

@Component({
    selector: 'app-teacher-item',
    templateUrl: './teacher-item.component.html',
    styleUrl: './teacher-item.component.scss',
    standalone: false
})
export class TeacherItemComponent {
  @Input({ required: true }) teacher: Teacher;
  @Output() teacherEditClicked: EventEmitter<Teacher> = new EventEmitter();
  @Output() teacherDeleteClicked: EventEmitter<Teacher> = new EventEmitter();
}
