import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { StudentGroup } from '../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { SearchDirectionsService } from '../../../education-objects-page/education-directions-inline-list/search-directions.service';
import { AttachPlanGroupService } from './attach-plan-group.service';
import { UnauthorizedErrorHandler } from '../../../../shared/models/common/401-error-handler/401-error-handler.service';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { EducationPlan } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { catchError, Observable, tap } from 'rxjs';
import { StudentPageViewModel } from '../../student-page-viewmodel.service';
import { HttpErrorResponse } from '@angular/common/http';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';

@Component({
  selector: 'app-attach-plan-group-dialog',
  imports: [
    RedOutlineButtonComponent,
    NgForOf,
    NgIf,
    NgClass,
    GreenOutlineButtonComponent,
  ],
  templateUrl: './attach-plan-group-dialog.component.html',
  styleUrl: './attach-plan-group-dialog.component.scss',
  standalone: true,
})
export class AttachPlanGroupDialogComponent implements OnInit {
  @Input({ required: true }) group: StudentGroup;
  @Output() visibilityChange: EventEmitter<void> = new EventEmitter();
  public directions: EducationDirection[] = [];

  public selectedEducationDirection: EducationDirection | null = null;
  public selectedEducationPlan: EducationPlan | null = null;
  public selectedSemesterNumber: number | null = null;

  public constructor(
    private readonly _notifications: NotificationService,
    private readonly _directionsDataService: SearchDirectionsService,
    private readonly _service: AttachPlanGroupService,
    private readonly _handler: UnauthorizedErrorHandler,
    private readonly _viewModel: StudentPageViewModel,
  ) {}

  public ngOnInit() {
    this._directionsDataService
      .getAll()
      .pipe(
        tap((response) => {
          this.directions = response;
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  public attach(): void {
    if (!this.selectedEducationDirection) {
      this._notifications.bulkFailure(
        'Направление подготовки должно быть выбрано',
      );
      return;
    }
    if (!this.selectedEducationPlan) {
      this._notifications.bulkFailure('Учебный план должен быть выбран');
      return;
    }
    if (!this.selectedSemesterNumber) {
      this._notifications.bulkFailure('Номер семестра должен быть выбран');
      return;
    }
    this._service
      .attach(
        this.group,
        this.selectedEducationDirection,
        this.selectedEducationPlan,
        this.selectedSemesterNumber,
      )
      .pipe(
        tap((response) => {
          this._notifications.bulkSuccess('Закреплён учебный план у группы');
          this._viewModel.attachEducationPlan(this.group, response);
          this.visibilityChange.emit();
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          this._notifications.bulkFailure(error.error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  public selectDirection(
    direction: EducationDirection,
    $event: MouseEvent,
  ): void {
    $event.stopPropagation();
    this.selectedEducationDirection = direction;
  }

  public selectEducationPlan(plan: EducationPlan, $event: MouseEvent): void {
    $event.stopPropagation();
    this.selectedEducationPlan = plan;
  }

  public selectSemesterNumber(value: number, $event: MouseEvent): void {
    $event.stopPropagation();
    this.selectedSemesterNumber = value;
  }
}
