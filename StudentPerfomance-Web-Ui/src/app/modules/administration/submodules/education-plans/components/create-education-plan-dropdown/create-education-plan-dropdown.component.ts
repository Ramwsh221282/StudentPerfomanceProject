import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EducationDirection } from '../../../education-directions/models/education-direction-interface';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { CreateService } from '../../services/create.service';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { EducationPlanCreationHandler } from './education-plan-creation-handler';
import { EducationPlan } from '../../models/education-plan-interface';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-create-education-plan-dropdown',
  templateUrl: './create-education-plan-dropdown.component.html',
  styleUrl: './create-education-plan-dropdown.component.scss',
})
export class CreateEducationPlanDropdownComponent implements ISubbmittable {
  public number: string = '';
  public directionData: string = 'Выбрать направление подготовки';
  public direction: EducationDirection | null;
  protected isSelectionDirections: boolean = false;
  @Input({ required: true }) visibility: boolean;
  @Output() visibilityChanged: EventEmitter<boolean> = new EventEmitter();
  @Output() refreshRequested: EventEmitter<void> = new EventEmitter();

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _creationService: CreateService,
  ) {}

  public submit(): void {
    if (this.isDirectionEmpty()) return;
    if (this.isYearEmpty()) return;
    const plan = {} as EducationPlan;
    plan.year = Number(this.number);
    plan.direction = { ...this.direction! };
    const handler = EducationPlanCreationHandler(
      this._notificationService,
      this.refreshRequested,
    );
    this._creationService
      .create(plan)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.close();
        }),
        catchError((error: HttpErrorResponse) => {
          this.close();
          return handler.handleError(error);
        }),
      )
      .subscribe();
  }

  protected close(): void {
    this.visibility = false;
    this.visibilityChanged.emit(this.visibility);
  }

  private isDirectionEmpty(): boolean {
    if (this.direction == null) {
      this._notificationService.SetMessage =
        'Направление подготовки не выбрано';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isYearEmpty(): boolean {
    if (this.number.length == 0 || this.number.trim().length == 0) {
      this._notificationService.SetMessage = 'Год учебного плана не указан';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  protected onDirectionDataSelected(data: string) {
    this.createDirection(this.parseData(data));
  }

  private createDirection(data: ParsedDirectionData): void {
    this.direction = {} as EducationDirection;
    this.direction.name = data.name;
    this.direction.code = data.code;
    this.direction.type = data.type;
  }

  private parseData(data: string): ParsedDirectionData {
    const nameMatch = data.match(/^\D+/);
    const name = nameMatch![0].trim();
    const remaining = data.substring(name.length).trim();
    const [code, type] = remaining.split(/\s+/);
    return { name, code, type };
  }
}

type ParsedDirectionData = {
  name: string;
  code: string;
  type: string;
};
