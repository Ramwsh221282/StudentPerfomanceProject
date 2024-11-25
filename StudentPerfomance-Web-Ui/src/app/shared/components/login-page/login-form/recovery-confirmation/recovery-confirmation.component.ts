import { Component, OnInit } from '@angular/core';
import { RecoveryConfirmationService } from './recovery-confirmation-service';
import { ActivatedRoute } from '@angular/router';
import { catchError, Observable, tap } from 'rxjs';

@Component({
  selector: 'app-recovery-confirmation',
  standalone: true,
  imports: [],
  templateUrl: './recovery-confirmation.component.html',
  styleUrl: './recovery-confirmation.component.scss',
  providers: [RecoveryConfirmationService],
})
export class RecoveryConfirmationComponent implements OnInit {
  protected text: string = '';

  public constructor(
    private readonly _service: RecoveryConfirmationService,
    private readonly _activatedRoute: ActivatedRoute,
  ) {}

  public ngOnInit(): void {
    this._activatedRoute.queryParams.subscribe((params) => {
      const token = params['token'];
      this.confirmPasswordRecovery(token);
    });
  }

  private confirmPasswordRecovery(token: string): void {
    this._service
      .confirmPasswordRecovery(token)
      .pipe(
        tap((response) => {
          this.text = 'Пароль сброшен. Новый пароль отправлен на почту';
        }),
        catchError((error) => {
          this.text = error.error;
          return new Observable();
        }),
      )
      .subscribe();
  }
}
