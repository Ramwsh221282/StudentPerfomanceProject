import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EducationPlan } from '../../../../../education-plans/models/education-plan-interface';
import { ISubbmittable } from '../../../../../../../../shared/models/interfaces/isubbmitable';
import { EducationPlanAttachmentService } from '../education-plan-attachment.service';
import { UserOperationNotificationService } from '../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroup } from '../../../../services/studentsGroup.interface';
import { catchError, tap } from 'rxjs';
import { EducationPlanAttachmentHandler } from '../education-plan-attachment-handler';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-semester-number-selection-modal',
  templateUrl: './semester-number-selection-modal.component.html',
  styleUrl: './semester-number-selection-modal.component.scss',
})
export class SemesterNumberSelectionModalComponent
  implements OnInit, ISubbmittable
{
  @Output() visibility: EventEmitter<void> = new EventEmitter();
  @Output() success: EventEmitter<void> = new EventEmitter();
  @Output() failure: EventEmitter<void> = new EventEmitter();
  @Input({ required: true }) educationPlan: EducationPlan;
  @Input({ required: true }) group: StudentGroup;

  protected semesterNumbers: number[] = [];
  protected selectedSemester: number = 1;

  public constructor(
    private readonly _attachmentService: EducationPlanAttachmentService,
    protected readonly notificationService: UserOperationNotificationService,
  ) {}

  public submit(): void {
    const handler = EducationPlanAttachmentHandler(
      this.notificationService,
      this.success,
      this.failure,
      this.visibility,
    );
    this._attachmentService
      .attachPlan(this.group, this.educationPlan, this.selectedSemester)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.group = { ...response };
          this.educationPlan = {} as EducationPlan;
        }),
        catchError((error: HttpErrorResponse) => handler.handleError(error)),
      )
      .subscribe();
  }

  public ngOnInit(): void {
    this.InitializeSemesterNumbers();
  }

  protected selectSemesterNumber(number: any): void {
    this.selectedSemester = number.target.value!;
  }

  private InitializeSemesterNumbers(): void {
    if (this.educationPlan.direction.type == 'Бакалавриат')
      this.InitializeAsBachelor();
    else this.InitializeAsMagister();
  }

  private InitializeAsBachelor(): void {
    const limit: number = 8;
    for (let i = 1; i <= limit; i++) {
      this.semesterNumbers.push(i);
    }
  }

  private InitializeAsMagister(): void {
    const limit: number = 4;
    for (let i = 1; i <= limit; i++) {
      this.semesterNumbers.push(i);
    }
  }
}
