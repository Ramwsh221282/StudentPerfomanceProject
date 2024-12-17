import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SemesterDiscipline } from '../../../../../../models/education-plan-interface';
import { ISubbmittable } from '../../../../../../../../../../shared/models/interfaces/isubbmitable';
import { UserOperationNotificationService } from '../../../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-change-discipline-name-popup',
  templateUrl: './change-discipline-name-popup.component.html',
  styleUrl: './change-discipline-name-popup.component.scss',
})
export class ChangeDisciplineNamePopupComponent
  implements OnInit, ISubbmittable
{
  @Input({ required: true }) discipline: SemesterDiscipline;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  @Output() disciplineNameUpdateCommited: EventEmitter<SemesterDiscipline[]> =
    new EventEmitter();
  protected disciplineCopy: SemesterDiscipline;

  public constructor(
    private readonly _service: UserOperationNotificationService,
  ) {}

  public ngOnInit(): void {
    this.disciplineCopy = { ...this.discipline };
  }

  public submit(): void {
    if (this.isNewDisciplineNameEmpty()) return;
    this.disciplineNameUpdateCommited.emit([
      this.disciplineCopy,
      this.discipline,
    ]);
    this.visibilityChanged.emit();
  }

  private isNewDisciplineNameEmpty(): boolean {
    if (
      this.disciplineCopy.disciplineName.length == 0 ||
      this.disciplineCopy.disciplineName.trim().length == 0
    ) {
      this._service.SetMessage = 'Название дисциплины пустое';
      this._service.failure();
      return true;
    }
    return false;
  }
}
