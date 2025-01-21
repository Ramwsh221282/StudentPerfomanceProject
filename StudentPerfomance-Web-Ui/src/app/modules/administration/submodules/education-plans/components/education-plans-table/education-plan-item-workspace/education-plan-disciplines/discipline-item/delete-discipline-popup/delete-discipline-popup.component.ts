import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SemesterDiscipline } from '../../../../../../models/education-plan-interface';
import { ISubbmittable } from '../../../../../../../../../../shared/models/interfaces/isubbmitable';

@Component({
    selector: 'app-delete-discipline-popup',
    templateUrl: './delete-discipline-popup.component.html',
    styleUrl: './delete-discipline-popup.component.scss',
    standalone: false
})
export class DeleteDisciplinePopupComponent implements ISubbmittable {
  @Input({ required: true }) discipline: SemesterDiscipline;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  @Output() disciplineDeletionCommited: EventEmitter<SemesterDiscipline> =
    new EventEmitter();

  public submit(): void {
    this.disciplineDeletionCommited.emit(this.discipline);
    this.visibilityChanged.emit();
  }
}
