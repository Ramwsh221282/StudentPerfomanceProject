import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AddIconButtonComponent } from '../../../building-blocks/buttons/add-icon-button/add-icon-button.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { Department } from '../../../modules/administration/submodules/departments/models/departments.interface';
import { TeachersDepartmentsDataService } from '../../education-objects-page/disciplines-list/edit-discipline-dialog/discipline-teacher-part/discipline-departments-list/teachers-departments-data.service';
import { DepartmentItemComponent } from './department-item/department-item.component';
import { NgForOf, NgIf } from '@angular/common';
import { DepartmentCreateDialogComponent } from './department-create-dialog/department-create-dialog.component';
import { DepartmentRemoveDialogComponent } from './department-remove-dialog/department-remove-dialog.component';
import { DepartmentEditDialogComponent } from './department-edit-dialog/department-edit-dialog.component';

@Component({
  selector: 'app-departments-list',
  imports: [
    AddIconButtonComponent,
    GreenOutlineButtonComponent,
    DepartmentItemComponent,
    NgForOf,
    DepartmentCreateDialogComponent,
    NgIf,
    DepartmentRemoveDialogComponent,
    DepartmentEditDialogComponent,
  ],
  templateUrl: './departments-list.component.html',
  styleUrl: './departments-list.component.scss',
  standalone: true,
})
export class DepartmentsListComponent implements OnInit {
  @Output() departmentSelected: EventEmitter<Department> = new EventEmitter();
  @Output() departmentRemoved: EventEmitter<void> = new EventEmitter();
  public isCreatingDepartment: boolean = false;
  public departments: Department[];
  public selectedDepartment: Department | null;
  public editDepartmentRequest: Department | null;
  public removeDepartmentRequest: Department | null;

  public constructor(
    private readonly _dataService: TeachersDepartmentsDataService,
  ) {}

  public handleDepartmentSelection(department: Department): void {
    this.selectedDepartment = department;
    this.departmentSelected.emit(department);
  }

  public handleDepartmentCreation(department: Department): void {
    this.departments.push(department);
  }

  public handleDepartmentRemoved(department: Department): void {
    this.departments.splice(this.departments.indexOf(department), 1);
    if (
      this.selectedDepartment &&
      this.selectedDepartment.name == department.name
    ) {
      this.selectedDepartment = null;
      this.departmentRemoved.emit();
    }
  }

  public ngOnInit() {
    this._dataService.getDepartments().subscribe((response) => {
      this.departments = response;
    });
  }
}
