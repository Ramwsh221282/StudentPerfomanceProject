import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AddIconButtonComponent } from '../../../building-blocks/buttons/add-icon-button/add-icon-button.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { Department } from '../../../modules/administration/submodules/departments/models/departments.interface';
import { TeachersDepartmentsDataService } from '../../education-objects-page/disciplines-list/edit-discipline-dialog/discipline-teacher-part/discipline-departments-list/teachers-departments-data.service';
import { DepartmentItemComponent } from './department-item/department-item.component';
import { NgForOf } from '@angular/common';

@Component({
  selector: 'app-departments-list',
  imports: [
    AddIconButtonComponent,
    GreenOutlineButtonComponent,
    DepartmentItemComponent,
    NgForOf,
  ],
  templateUrl: './departments-list.component.html',
  styleUrl: './departments-list.component.scss',
  standalone: true,
})
export class DepartmentsListComponent implements OnInit {
  @Output() departmentSelected: EventEmitter<Department> = new EventEmitter();
  public departments: Department[];
  public selectedDepartment: Department | null;

  public constructor(
    private readonly _dataService: TeachersDepartmentsDataService,
  ) {}

  public handleDepartmentSelection(department: Department): void {
    this.selectedDepartment = department;
    this.departmentSelected.emit(department);
  }

  public ngOnInit() {
    this._dataService.getDepartments().subscribe((response) => {
      this.departments = response;
    });
  }
}
