import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FacadeService } from '../../services/facade.service';
import { EducationPlan } from '../../models/education-plan-interface';
import { EducationPlanDeletionHandler } from './education-plan-deletion-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-education-plan-deletion-modal',
  templateUrl: './education-plan-deletion-modal.component.html',
  styleUrl: './education-plan-deletion-modal.component.scss',
})
export class EducationPlanDeletionModalComponent {
  @Input({ required: true }) plan: EducationPlan;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();

  public constructor(private readonly _facadeService: FacadeService) {}

  protected confirm(): void {
    const handler = EducationPlanDeletionHandler(this._facadeService);
    this._facadeService
      .delete(this.plan)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.visibility.emit(false);
  }

  protected decline(): void {
    this.visibility.emit(false);
  }
}
