import { Component, EventEmitter, Output } from '@angular/core';
import { IsNullOrWhiteSpace } from '../../../../shared/utils/string-helper';
import { CreateGroupService } from './create-group.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { StudentGroup } from '../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { NgIf } from '@angular/common';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { SelectJobTitleDropdownComponent } from '../../../teachers-departments-page/teachers-list/create-teacher-dialog/select-job-title-dropdown/select-job-title-dropdown.component';
import { SelectStateDropdownComponent } from '../../../teachers-departments-page/teachers-list/create-teacher-dialog/select-state-dropdown/select-state-dropdown.component';

@Component({
  selector: 'app-create-group-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    NgIf,
    RedOutlineButtonComponent,
    SelectJobTitleDropdownComponent,
    SelectStateDropdownComponent,
  ],
  templateUrl: './create-group-dialog.component.html',
  styleUrl: './create-group-dialog.component.scss',
  standalone: true,
})
export class CreateGroupDialogComponent {
  @Output() groupCreated: EventEmitter<StudentGroup> = new EventEmitter();
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  public name: string = '';

  public constructor(
    private readonly _service: CreateGroupService,
    private readonly _notifications: NotificationService,
  ) {}

  public create(): void {
    if (IsNullOrWhiteSpace(this.name)) {
      this._notifications.setMessage('Название группы должно быть указано');
      this._notifications.failure();
      this._notifications.turn();
      return;
    }
    const group = {} as StudentGroup;
    group.name = this.name;
    this._service
      .create(group)
      .pipe(
        tap((response) => {
          this._notifications.setMessage('Добавлена новая студенческая группа');
          this._notifications.success();
          this._notifications.turn();
          this.groupCreated.emit(response);
          this.cleanInputs();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notifications.setMessage(error.error);
          this._notifications.failure();
          this._notifications.turn();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  private cleanInputs(): void {
    this.name = '';
  }
}
