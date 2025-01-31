import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Teacher } from '../../../../modules/administration/submodules/teachers/models/teacher.interface';
import { RegisterTeacherService } from './register-teacher.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { IsNullOrWhiteSpace } from '../../../../shared/utils/string-helper';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { NgIf } from '@angular/common';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-register-teacher-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    NgIf,
    RedOutlineButtonComponent,
  ],
  templateUrl: './register-teacher-dialog.component.html',
  styleUrl: './register-teacher-dialog.component.scss',
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
export class RegisterTeacherDialogComponent {
  @Input({ required: true }) registerTeacher: Teacher;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  public email: string = '';

  public constructor(
    private readonly _service: RegisterTeacherService,
    private readonly _notifications: NotificationService,
  ) {}

  public register(): void {
    if (IsNullOrWhiteSpace(this.email)) {
      this._notifications.setMessage('Почта должна быть указана');
      this._notifications.failure();
      this._notifications.turn();
      return;
    }
    this._service
      .register(this.registerTeacher, this.email)
      .pipe(
        tap((response) => {
          this._notifications.setMessage('Преподаватель был зарегистрирован');
          this._notifications.success();
          this._notifications.turn();
          this.registerTeacher.userId = response.userId;
          this.visibilityChanged.emit();
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
}
