import { Component, Input } from '@angular/core';
import { RemoveIconButtonComponent } from '../../../../building-blocks/buttons/remove-icon-button/remove-icon-button.component';
import { EditIconButtonComponent } from '../../../../building-blocks/buttons/edit-icon-button/edit-icon-button.component';
import { NgIf, NgOptimizedImage } from '@angular/common';
import { SemesterDiscipline } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';

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
})
export class DisciplineItemComponent {
  @Input({ required: true }) discipline: SemesterDiscipline;
}
