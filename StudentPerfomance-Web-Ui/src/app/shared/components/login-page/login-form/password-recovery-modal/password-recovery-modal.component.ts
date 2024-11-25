import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ISubbmittable } from '../../../../models/interfaces/isubbmitable';
import { UserOperationNotificationService } from '../../../../services/user-notifications/user-operation-notification-service.service';
import { PasswordRecoveryService } from './password-recovery.service';
import { catchError, Observable, tap } from 'rxjs';

@Component({
  selector: 'app-password-recovery-modal',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule],
  templateUrl: './password-recovery-modal.component.html',
  styleUrl: './password-recovery-modal.component.scss',
  providers: [PasswordRecoveryService],
})
export class PasswordRecoveryModalComponent implements ISubbmittable {
  @Output() visibilityEmitter: EventEmitter<void> = new EventEmitter();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();
  protected email: string = '';

  public constructor(
    private readonly notificationService: UserOperationNotificationService,
    private readonly _recoveryService: PasswordRecoveryService,
  ) {}

  public submit(): void {
    this._recoveryService
      .requestPasswordRecovery(this.email)
      .pipe(
        tap((response) => {
          this.notificationService.SetMessage =
            'Инструкции были отправлены на указанную почту';
          this.email = '';
          this.successEmitter.emit();
        }),
        catchError((error) => {
          this.notificationService.SetMessage = error.error;
          this.failureEmitter.emit();
          this.email = '';
          return new Observable();
        }),
      )
      .subscribe();
  }
}
