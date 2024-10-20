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
  protected directions: EducationDirection[];

  protected isFailure: boolean;
  protected isSuccess: boolean;

  protected activeDirection: EducationDirection;

  public constructor(
    protected readonly facadeService: FacadeService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    this.activeDirection = {} as EducationDirection;
    this.directions = [];
    this.isFailure = false;
    this.isSuccess = false;
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
          this.isFailure = true;
          return new Observable();
        })
      )
      .subscribe();
  }

  protected refreshActiveDirection(): void {
    this.activeDirection = {} as EducationDirection;
  }

  protected startCreation(): void {
    this.creationModalVisibility = true;
  }

  protected stopCreation(value: boolean): void {
    this.creationModalVisibility = value;
  }

  protected startFilter(): void {
    this.filterModalVisibility = true;
  }

  protected stopFilter(value: boolean): void {
    this.filterModalVisibility = value;
  }
}
