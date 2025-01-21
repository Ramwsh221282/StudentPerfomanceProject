import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FacadeService } from '../../services/facade.service';
import { EducationDirection } from '../../models/education-direction-interface';
import { AuthService } from '../../../../../../pages/user-page/services/auth.service';
import { AppConfigService } from '../../../../../../app.config.service';
import { FilterFetchPolicy } from '../../models/fetch-policies/filter-fetch-policy';
import { DefaultFetchPolicy } from '../../models/fetch-policies/default-fetch-policy';

@Component({
    selector: 'app-education-direction-filter-dropdown',
    templateUrl: './education-direction-filter-dropdown.component.html',
    styleUrl: './education-direction-filter-dropdown.component.scss',
    standalone: false
})
export class EducationDirectionFilterDropdownComponent {
  @Input() visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();
  protected directionName: string = '';
  protected directionCode: string = '';
  protected directionType: string = '';
  protected isSelectingType = false;

  public constructor(
    private readonly _facadeService: FacadeService,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {}

  protected submit(): void {
    const direction = this.createEducationDirection();
    const policy = new FilterFetchPolicy(
      direction,
      this._authService,
      this._appConfig,
    );
    this._facadeService.setFetchPolicy(policy);
    this.refreshEmitter.emit();
    this.closeDropdown();
  }

  protected cancelFilter(): void {
    const policy = new DefaultFetchPolicy(this._authService, this._appConfig);
    this._facadeService.setFetchPolicy(policy);
    this.refreshEmitter.emit();
    this.closeDropdown();
  }

  protected closeDropdown(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  private createEducationDirection(): EducationDirection {
    const direction = {} as EducationDirection;
    direction.name = this.directionName;
    direction.code = this.directionCode;
    direction.type = this.directionType;
    return direction;
  }
}
