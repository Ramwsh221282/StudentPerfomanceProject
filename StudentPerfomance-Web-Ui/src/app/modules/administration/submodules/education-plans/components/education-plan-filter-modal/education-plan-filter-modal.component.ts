import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FacadeService } from '../../services/facade.service';
import { FilterFetchPolicy } from '../../models/fetch-policies/filter-fetch-policy';
import { DefaultFetchPolicy } from '../../models/fetch-policies/default-fetch-policy';
import { EducationPlanFilterBaseForm } from './education-plan-filter-base-form';

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

  public constructor(private readonly _facadeService: FacadeService) {
    super();
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected applyFilter(): void {
    const plan = this.createPlanFromForm();
    const policy = new FilterFetchPolicy(plan);
    this._facadeService.setFetchPolicy(policy);
    this._facadeService.fetch();
  }

  protected cancel(): void {
    this.initForm();
    const policy = new DefaultFetchPolicy();
    this._facadeService.setFetchPolicy(policy);
    this._facadeService.fetch();
    this.visibility.emit(false);
  }
}
