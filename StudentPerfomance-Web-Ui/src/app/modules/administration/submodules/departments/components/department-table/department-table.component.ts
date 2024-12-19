import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Department } from '../../models/departments.interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-department-table',
  templateUrl: './department-table.component.html',
  styleUrl: './department-table.component.scss',
})
export class DepartmentTableComponent {
  @Input({ required: true }) departments: Department[];
  @Output() departmentSelected: EventEmitter<Department> = new EventEmitter();
  @Output() departmentAdded: EventEmitter<Department> = new EventEmitter();
  @Output() departmentRemoved: EventEmitter<Department> = new EventEmitter();
  @Output() filtered: EventEmitter<void> = new EventEmitter();

  protected currentlySelectedDepartment: Department | null;
  protected isCreatingDepartment: boolean = false;

  protected departmentToRemove: Department | null = null;
  protected isDeletingDepartment: boolean = false;

  protected isFiltering: boolean = false;

  public constructor(private readonly _router: Router) {}

  protected navigateToDocumentation(): void {
    const path = ['/departments-info'];
    this._router.navigate(path);
  }

  protected manageSelectedDepartment(department: Department): boolean {
    if (!this.currentlySelectedDepartment) return false;
    return department.name == this.currentlySelectedDepartment.name;
  }

  protected handleCreatedDepartment(department: Department): void {
    this.departmentAdded.emit(department);
  }

  protected handleDeletedDepartment(department: Department): void {
    this.departmentRemoved.emit(department);
  }

  protected handleFilter(): void {
    this.filtered.emit();
  }
}
