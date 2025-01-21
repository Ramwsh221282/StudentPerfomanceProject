import { Component, OnInit } from '@angular/core';
import { FacadeService } from '../../services/facade.service';
import { EducationPlan } from '../../models/education-plan-interface';
import { catchError, Observable, tap } from 'rxjs';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
    selector: 'app-education-plans-table',
    templateUrl: './education-plans-table.component.html',
    styleUrl: './education-plans-table.component.scss',
    standalone: false
})
export class EducationPlansTableComponent implements OnInit {
  protected creationModalVisibility = false;
  protected filterModalVisibility = false;
  protected plans: EducationPlan[];
  protected selectedPlan: EducationPlan | null = null;
  protected selectedPlanForDeletion: EducationPlan | null = null;

  public constructor(
    protected readonly facadeService: FacadeService,
    protected readonly notificationService: UserOperationNotificationService,
  ) {
    this.plans = [];
  }

  public ngOnInit(): void {
    this.facadeService
      .fetch()
      .pipe(
        tap((response) => {
          this.plans = response;
        }),
        catchError((error) => {
          return new Observable();
        }),
      )
      .subscribe();
  }
}
