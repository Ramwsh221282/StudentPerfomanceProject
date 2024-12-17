import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../services/studentsGroup.interface';

@Component({
  selector: 'app-student-group-item',
  templateUrl: './student-group-item.component.html',
  styleUrl: './student-group-item.component.scss',
})
export class StudentGroupItemComponent {
  @Input({ required: true }) isCurrentlySelected: boolean = false;
  @Input({ required: true }) group: StudentGroup;
  @Output() selected: EventEmitter<StudentGroup> = new EventEmitter();
}
