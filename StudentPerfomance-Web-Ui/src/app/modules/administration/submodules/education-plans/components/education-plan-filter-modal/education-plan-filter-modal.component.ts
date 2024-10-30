import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FacadeService } from '../../services/facade.service';
import { FilterFetchPolicy } from '../../models/fetch-policies/filter-fetch-policy';
import { DefaultFetchPolicy } from '../../models/fetch-policies/default-fetch-policy';
import { EducationPlanFilterBaseForm } from './education-plan-filter-base-form';
import { AuthService } from '../../../../../users/services/auth.service';
import { EducationPlan } from '../../models/education-plan-interface';
import { catchError, Observable, tap } from 'rxjs';

@Component({
  selector: 'app-education-plan-filter-modal',
  templateUrl: './education-plan-filter-modal.component.html',
  styleUrl: './education-plan-filter-modal.component.scss',
})
export class EducationPlanFilterModalComponent
  extends EducationPlanFilterBaseForm
  implements OnInit
{
  @Output() visibility: EventEmitter<boolean> = new EventEmitter();
  @Output() filteredData: EventEmitter<EducationPlan[]> = new EventEmitter();
  @Output() failure: EventEmitter<void> = new EventEmitter();

  public constructor(
    private readonly _facadeService: FacadeService,
    private readonly _authService: AuthService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected applyFilter(): void {
    const plan = this.createPlanFromForm();
    console.log(plan);
    const policy = new FilterFetchPolicy(plan, this._authService);
    this._facadeService.setFetchPolicy(policy);
    this._facadeService
      .fetch()
      .pipe(
        tap((response) => {
          this.filteredData.emit(response);
          this.visibility.emit(false);
        }),
        catchError((error) => {
          this.failure.emit();
          this.visibility.emit(false);
          return new Observable();
        })
      )
      .subscribe();
  }

  protected cancel(): void {
    this.initForm();
    const policy = new DefaultFetchPolicy(this._authService);
    this._facadeService.setFetchPolicy(policy);
    this._facadeService
      .fetch()
      .pipe(
        tap((response) => {
          this.filteredData.emit(response);
          this.visibility.emit(false);
        }),
        catchError((error) => {
          this.failure.emit();
          this.visibility.emit(false);
          return new Observable();
        })
      )
      .subscribe();
  }
}
