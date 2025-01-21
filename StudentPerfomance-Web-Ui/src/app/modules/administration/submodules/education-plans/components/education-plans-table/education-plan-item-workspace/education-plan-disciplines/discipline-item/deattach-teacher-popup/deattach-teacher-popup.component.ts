import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SemesterDiscipline } from '../../../../../../models/education-plan-interface';

@Component({
    selector: 'app-deattach-teacher-popup',
    templateUrl: './deattach-teacher-popup.component.html',
    styleUrl: './deattach-teacher-popup.component.scss',
    standalone: false
})
export class DeattachTeacherPopupComponent {
  @Input({ required: true }) discipline: SemesterDiscipline;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  @Output() deattachmentCommited: EventEmitter<SemesterDiscipline> =
    new EventEmitter();

  protected commit(): void {
    this.deattachmentCommited.emit(this.discipline);
    this.visibilityChanged.emit();
  }
}
