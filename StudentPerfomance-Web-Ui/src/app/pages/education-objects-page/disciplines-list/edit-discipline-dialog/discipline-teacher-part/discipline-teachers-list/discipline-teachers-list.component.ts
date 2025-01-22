import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgForOf, NgIf } from '@angular/common';
import { Teacher } from '../../../../../../modules/administration/submodules/teachers/models/teacher.interface';

@Component({
  selector: 'app-discipline-teachers-list',
  imports: [NgForOf, NgIf],
  templateUrl: './discipline-teachers-list.component.html',
  styleUrl: './discipline-teachers-list.component.scss',
  standalone: true,
})
export class DisciplineTeachersListComponent {
  @Output() teacherSelected: EventEmitter<Teacher> = new EventEmitter();
  @Input({ required: true }) teachers: Teacher[] = [];

  public handleTeacherSelect(teacher: Teacher, $event: MouseEvent) {
    $event.stopPropagation();
    this.teacherSelected.emit(teacher);
  }
}
