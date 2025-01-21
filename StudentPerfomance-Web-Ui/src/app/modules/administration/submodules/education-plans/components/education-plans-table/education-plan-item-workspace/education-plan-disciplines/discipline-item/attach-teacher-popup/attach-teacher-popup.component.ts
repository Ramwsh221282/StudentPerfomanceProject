import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Department } from '../../../../../../../departments/models/departments.interface';
import { DepartmentDataService } from '../../../../../../../departments/components/department-table/department-data.service';
import { Teacher } from '../../../../../../../teachers/models/teacher.interface';

@Component({
    selector: 'app-attach-teacher-popup',
    templateUrl: './attach-teacher-popup.component.html',
    styleUrl: './attach-teacher-popup.component.scss',
    standalone: false
})
export class AttachTeacherPopupComponent implements OnInit {
  protected departments: Department[];
  protected selectedDepartment: Department | null = null;
  protected teachersOfSelectedDepartment: Teacher[] = [];
  protected selectedTeacher: Teacher | null = null;
  protected isSelectingDepartment: boolean = false;
  protected isSelectingTeacher: boolean = false;
  protected departmentData: string = '';
  protected teacherData: string = '';

  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  @Output() selectedTeacherForAttachment: EventEmitter<Teacher> =
    new EventEmitter();

  public constructor(
    private readonly _departmentDataService: DepartmentDataService,
  ) {}

  public ngOnInit(): void {
    this._departmentDataService.getAllDepartments().subscribe((response) => {
      this.departments = response;
    });
  }

  protected getDepartmentNames(): string[] {
    return this.departments.map((department) => department.name);
  }

  protected onDepartmentNameSelected(name: string): void {
    this.departmentData = name;
    this.selectedDepartment = this.departments.find(
      (department) => department.name == name,
    )!;
    this.teachersOfSelectedDepartment = this.selectedDepartment!.teachers;
  }

  protected onTeacherNameSelected(names: string): void {
    this.teacherData = names;
    const splittedNames = names.split(' ');
    const surname = splittedNames[0];
    const name = splittedNames[1];
    const patronymic = splittedNames[2] == '' ? '' : splittedNames[2];
    this.selectedTeacher = this.teachersOfSelectedDepartment.find(
      (teacher) =>
        teacher.name == name &&
        teacher.surname == surname &&
        teacher.patronymic == patronymic,
    )!;
    this.selectedTeacher.department = { ...this.selectedDepartment! };
  }

  protected getTeacherNames(): string[] {
    const names: string[] = [];
    for (const teacher of this.teachersOfSelectedDepartment) {
      const name = teacher.name;
      const surname = teacher.surname;
      const patronymic = teacher.patronymic == null ? '' : teacher.patronymic;
      const joinedName = `${surname} ${name} ${patronymic}`;
      names.push(joinedName);
    }
    return names;
  }
}
