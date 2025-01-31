import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RemoveIconButtonComponent } from '../../../../building-blocks/buttons/remove-icon-button/remove-icon-button.component';
import { EditIconButtonComponent } from '../../../../building-blocks/buttons/edit-icon-button/edit-icon-button.component';
import { NgIf, NgOptimizedImage } from '@angular/common';
import { SemesterDiscipline } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-discipline-item',
  imports: [
    RemoveIconButtonComponent,
    EditIconButtonComponent,
    NgOptimizedImage,
    NgIf,
  ],
  templateUrl: './discipline-item.component.html',
  styleUrl: './discipline-item.component.scss',
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
export class DisciplineItemComponent {
  @Input({ required: true }) discipline: SemesterDiscipline;
  @Output() selectForEdit: EventEmitter<SemesterDiscipline> =
    new EventEmitter();
  @Output() selectForRemove: EventEmitter<SemesterDiscipline> =
    new EventEmitter();
}
