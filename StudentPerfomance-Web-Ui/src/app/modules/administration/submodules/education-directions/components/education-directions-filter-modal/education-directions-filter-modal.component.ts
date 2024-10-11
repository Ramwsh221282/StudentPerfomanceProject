import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { EducationDirectionBaseForm } from '../../models/education-direction-base-form';
import { FacadeService } from '../../services/facade.service';
import { FilterFetchPolicy } from '../../models/fetch-policies/filter-fetch-policy';
import { DefaultFetchPolicy } from '../../models/fetch-policies/default-fetch-policy';

@Component({
  selector: 'app-education-directions-filter-modal',
  templateUrl: './education-directions-filter-modal.component.html',
  styleUrl: './education-directions-filter-modal.component.scss',
})
export class EducationDirectionsFilterModalComponent
  extends EducationDirectionBaseForm
  implements OnInit
{
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();
  public constructor(private readonly _facadeService: FacadeService) {
    super();
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    const direction = this.createEducationDirectionFromForm();
    const policy = new FilterFetchPolicy(direction);
    this._facadeService.setFetchPolicy(policy);
    this._facadeService.fetch();
    this.close();
  }

  protected cancel(): void {
    const policy = new DefaultFetchPolicy();
    this._facadeService.setFetchPolicy(policy);
    this._facadeService.fetch();
    this.close();
  }

  protected close(): void {
    this.ngOnInit();
    this.visibility.emit(false);
  }
}
