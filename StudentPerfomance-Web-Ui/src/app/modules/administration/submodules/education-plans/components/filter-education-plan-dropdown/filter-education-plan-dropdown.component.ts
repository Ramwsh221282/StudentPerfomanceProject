import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EducationDirection } from '../../../education-directions/models/education-direction-interface';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { FacadeService } from '../../services/facade.service';
import { AppConfigService } from '../../../../../../app.config.service';
import { AuthService } from '../../../../../../pages/user-page/services/auth.service';
import { EducationPlan } from '../../models/education-plan-interface';
import { FilterFetchPolicy } from '../../models/fetch-policies/filter-fetch-policy';
import { DefaultFetchPolicy } from '../../models/fetch-policies/default-fetch-policy';

@Component({
    selector: 'app-filter-education-plan-dropdown',
    templateUrl: './filter-education-plan-dropdown.component.html',
    styleUrl: './filter-education-plan-dropdown.component.scss',
    standalone: false
})
export class FilterEducationPlanDropdownComponent implements ISubbmittable {
  public number: string = '';
  public directionData: string = 'Выбрать направление подготовки';
  public direction: EducationDirection | null;
  protected isSelectionDirections: boolean = false;
  @Input({ required: true }) visibility: boolean;
  @Output() visibilityChanged: EventEmitter<boolean> = new EventEmitter();
  @Output() refreshRequested: EventEmitter<void> = new EventEmitter();

  public constructor(
    private readonly _service: FacadeService,
    private readonly _appConfigService: AppConfigService,
    private readonly _authService: AuthService,
  ) {}

  public submit(): void {
    if (
      this.directionData == 'Выбрать направление подготовки' ||
      !this.direction
    ) {
      this.direction = {} as EducationDirection;
      this.direction.name = '';
      this.direction.code = '';
      this.direction.type = '';
    }
    const plan = {} as EducationPlan;
    plan.year = Number(this.number);
    plan.direction = this.direction;
    const policy = new FilterFetchPolicy(
      plan,
      this._authService,
      this._appConfigService,
    );
    this._service.setFetchPolicy(policy);
    this.cleanInputs();
    this.refreshRequested.emit();
    this.visibilityChanged.emit(false);
  }

  public cancel(): void {
    const policy = new DefaultFetchPolicy(
      this._authService,
      this._appConfigService,
    );
    this._service.setFetchPolicy(policy);
    this.refreshRequested.emit();
    this.visibilityChanged.emit(false);
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

  private cleanInputs(): void {
    this.direction = null;
    this.directionData = 'Выбрать направление подготовки';
    this.number = '';
  }
}

type ParsedDirectionData = {
  name: string;
  code: string;
  type: string;
};
