import { Component, EventEmitter, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { DepartmentDataService } from '../department-data.service';
import { Department } from '../../../models/departments.interface';
import { DepartmentBuilder } from '../../../models/builders/department-builder';
import { DepartmentFilterFetchPolicy } from '../../../models/fetch-policies/filter-fetch-policy';
import { DepartmentDefaultFetchPolicy } from '../../../models/fetch-policies/default-fetch-policy';

@Component({
  selector: 'app-department-filter-modal',
  templateUrl: './department-filter-modal.component.html',
  styleUrl: './department-filter-modal.component.scss',
})
export class DepartmentFilterModalComponent implements ISubbmittable {
  @Output() visibility: EventEmitter<boolean> = new EventEmitter();
  @Output() filterAccepted: EventEmitter<void> = new EventEmitter();

  private readonly _builder: DepartmentBuilder;
  protected department: Department;

  public constructor(private readonly _service: DepartmentDataService) {
    this._builder = new DepartmentBuilder();
    this.department = this._builder.buildDefault();
  }

  public submit(): void {
    this.department = { ...this._builder.buildInitialized(this.department) };
    this._service.setPolicy(new DepartmentFilterFetchPolicy(this.department));
    this.filterAccepted.emit();
    this.visibility.emit(false);
  }

  public cancelFilter(): void {
    this._service.setPolicy(new DepartmentDefaultFetchPolicy());
    this.filterAccepted.emit();
    this.visibility.emit(false);
  }
}
