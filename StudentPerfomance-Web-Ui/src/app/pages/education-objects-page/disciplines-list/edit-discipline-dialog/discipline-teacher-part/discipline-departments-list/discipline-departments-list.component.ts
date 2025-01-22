import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Department } from '../../../../../../modules/administration/submodules/departments/models/departments.interface';
import { TeachersDepartmentsDataService } from './teachers-departments-data.service';
import { NgForOf, NgIf } from '@angular/common';

@Component({
  selector: 'app-discipline-departments-list',
  imports: [NgForOf, NgIf],
  templateUrl: './discipline-departments-list.component.html',
  styleUrl: './discipline-departments-list.component.scss',
  standalone: true,
})
export class DisciplineDepartmentsListComponent implements OnInit {
  public departments: Department[] = [];
  @Output() departmentSelected: EventEmitter<Department> = new EventEmitter();

  public constructor(
    private readonly _dataService: TeachersDepartmentsDataService,
  ) {}

  public ngOnInit() {
    this._dataService.getDepartments().subscribe((response) => {
      this.departments = response;
    });
  }

  public selectDepartment(department: Department, $event: MouseEvent): void {
    $event.stopPropagation();
    this.departmentSelected.emit(department);
  }
}
