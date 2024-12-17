import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DropdownListComponent } from '../../../../../../../../../../../building-blocks/dropdown-list/dropdown-list.component';

@Component({
  selector: 'app-attach-teacher-select-teachers',
  standalone: true,
  imports: [DropdownListComponent],
  templateUrl: './attach-teacher-select-teachers.component.html',
  styleUrl: './attach-teacher-select-teachers.component.scss',
})
export class AttachTeacherSelectTeachersComponent {
  @Input({ required: true }) teacherNames: string[];
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  @Output() selectedTeacherName: EventEmitter<string> = new EventEmitter();
}
