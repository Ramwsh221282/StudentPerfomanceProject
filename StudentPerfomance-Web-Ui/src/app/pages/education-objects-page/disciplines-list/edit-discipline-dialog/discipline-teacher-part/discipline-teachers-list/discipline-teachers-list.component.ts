import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgForOf, NgIf } from '@angular/common';
import { Teacher } from '../../../../../../modules/administration/submodules/teachers/models/teacher.interface';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-discipline-teachers-list',
  imports: [NgForOf, NgIf],
  templateUrl: './discipline-teachers-list.component.html',
  styleUrl: './discipline-teachers-list.component.scss',
  standalone: true,
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(-10px)' }),
        animate(
          '300ms ease-out',
          style({ opacity: 1, transform: 'translateY(0)' }),
        ),
      ]),
      transition(':leave', [
        animate(
          '300ms ease-in',
          style({ opacity: 0, transform: 'translateY(-10px)' }),
        ),
      ]),
    ]),
  ],
})
export class DisciplineTeachersListComponent {
  @Output() teacherSelected: EventEmitter<Teacher> = new EventEmitter();
  @Input({ required: true }) teachers: Teacher[] = [];

  public handleTeacherSelect(teacher: Teacher, $event: MouseEvent) {
    $event.stopPropagation();
    this.teacherSelected.emit(teacher);
  }
}
