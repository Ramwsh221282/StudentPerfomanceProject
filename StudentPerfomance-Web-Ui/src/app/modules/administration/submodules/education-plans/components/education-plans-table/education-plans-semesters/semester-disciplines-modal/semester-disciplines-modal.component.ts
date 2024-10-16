import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SemesterDisciplinesDataService } from './semester-disciplines-data.service';
import { Semester } from '../../../../../semesters/models/semester.interface';
import { SemesterPlan } from '../../../../../semester-plans/models/semester-plan.interface';
import { UserOperationNotificationService } from '../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-semester-disciplines-modal',
  templateUrl: './semester-disciplines-modal.component.html',
  styleUrl: './semester-disciplines-modal.component.scss',
  providers: [SemesterDisciplinesDataService, UserOperationNotificationService],
})
export class SemesterDisciplinesModalComponent implements OnInit {
  @Input({ required: true }) semester: Semester;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter();

  protected semesterPlans: SemesterPlan[];
  protected activeSemesterPlan: SemesterPlan;

  protected creationModalState: boolean;
  protected deletionModalState: boolean;
  protected editModalState: boolean;

  protected isSuccess: boolean;
  protected isFailure: boolean;

  public constructor(
    private readonly _dataService: SemesterDisciplinesDataService,
    protected readonly _notificationService: UserOperationNotificationService
  ) {
    this.activeSemesterPlan = {} as SemesterPlan;
    this.semesterPlans = [];
    this.creationModalState = false;
    this.deletionModalState = false;
    this.isSuccess = false;
    this.isFailure = false;
  }

  public ngOnInit(): void {
    this.refreshSemesterPlans();
  }

  private refreshSemesterPlans(): void {
    this._dataService
      .getSemesterDisciplines(this.semester)
      .subscribe((response) => {
        this.semesterPlans = response;
      });
  }

  protected close(): void {
    this.visibility.emit(false);
  }

  protected startCreation(): void {
    this.creationModalState = true;
  }

  protected stopCreation(value: boolean): void {
    this.creationModalState = value;
  }

  protected startDeletion(semesterPlan: SemesterPlan): void {
    this.activeSemesterPlan = semesterPlan;
    this.deletionModalState = true;
  }

  protected stopDeletion(value: boolean): void {
    this.deletionModalState = value;
    this.activeSemesterPlan = {} as SemesterPlan;
  }

  protected updateTable(value: any): void {
    this.refreshSemesterPlans();
  }
}
