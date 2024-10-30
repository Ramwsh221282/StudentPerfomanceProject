import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { EducationDirectionBaseForm } from '../../models/education-direction-base-form';
import { FacadeService } from '../../services/facade.service';
import { FilterFetchPolicy } from '../../models/fetch-policies/filter-fetch-policy';
import { DefaultFetchPolicy } from '../../models/fetch-policies/default-fetch-policy';
import { AuthService } from '../../../../../users/services/auth.service';
import { EducationDirection } from '../../models/education-direction-interface';

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
  @Output() filteredData: EventEmitter<EducationDirection[]> =
    new EventEmitter();

  public constructor(
    private readonly _facadeService: FacadeService,
    private readonly _authService: AuthService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    const direction = this.createEducationDirectionFromForm();
    const policy = new FilterFetchPolicy(direction, this._authService);
    this._facadeService.setFetchPolicy(policy);
    this._facadeService.fetch().subscribe((response) => {
      this.filteredData.emit(response);
      this.close();
    });
  }

  protected cancel(): void {
    const policy = new DefaultFetchPolicy(this._authService);
    this._facadeService.setFetchPolicy(policy);
    this._facadeService.fetch().subscribe((response) => {
      this.filteredData.emit(response);
      this.close();
    });
  }

  protected close(): void {
    this.ngOnInit();
    this.visibility.emit(false);
  }
}
