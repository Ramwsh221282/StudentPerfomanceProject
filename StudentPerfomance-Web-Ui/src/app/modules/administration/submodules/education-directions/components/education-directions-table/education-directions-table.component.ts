import { Component, OnInit } from '@angular/core';
import { FacadeService } from '../../services/facade.service';
import { EducationDirection } from '../../models/education-direction-interface';
import { catchError, Observable, tap } from 'rxjs';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-education-directions-table',
  templateUrl: './education-directions-table.component.html',
  styleUrl: './education-directions-table.component.scss',
})
export class EducationDirectionsTableComponent implements OnInit {
  protected creationModalVisibility = false;
  protected filterModalVisibility = false;
  protected directionToRemove: EducationDirection | null;
  protected directions: EducationDirection[];

  protected activeDirection: EducationDirection;

  public constructor(
    protected readonly facadeService: FacadeService,
    protected readonly notificationService: UserOperationNotificationService,
  ) {
    this.activeDirection = {} as EducationDirection;
    this.directions = [];
  }

  public ngOnInit(): void {
    this.facadeService
      .fetch()
      .pipe(
        tap((response) => {
          this.directions = response;
        }),
        catchError((error) => {
          this.notificationService.SetMessage = error.error;
          return new Observable();
        }),
      )
      .subscribe();
  }
}
