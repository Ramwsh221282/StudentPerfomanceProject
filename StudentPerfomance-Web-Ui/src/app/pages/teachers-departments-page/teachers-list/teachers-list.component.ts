import { Component, Input } from '@angular/core';
import { AddIconButtonComponent } from '../../../building-blocks/buttons/add-icon-button/add-icon-button.component';
import { DepartmentItemComponent } from '../departments-list/department-item/department-item.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { NgForOf, NgIf } from '@angular/common';
import { Department } from '../../../modules/administration/submodules/departments/models/departments.interface';
import { TeacherCardComponent } from './teacher-card/teacher-card.component';
import { Teacher } from '../../../modules/administration/submodules/teachers/models/teacher.interface';
import { CreateTeacherDialogComponent } from './create-teacher-dialog/create-teacher-dialog.component';
import { RemoveTeacherDialogComponent } from './remove-teacher-dialog/remove-teacher-dialog.component';
import { EditTeacherDialogComponent } from './edit-teacher-dialog/edit-teacher-dialog.component';
import { RegisterTeacherDialogComponent } from './register-teacher-dialog/register-teacher-dialog.component';

@Component({
  selector: 'app-teachers-list',
  imports: [
    AddIconButtonComponent,
    DepartmentItemComponent,
    GreenOutlineButtonComponent,
    NgForOf,
    TeacherCardComponent,
    CreateTeacherDialogComponent,
    NgIf,
    RemoveTeacherDialogComponent,
    EditTeacherDialogComponent,
    RegisterTeacherDialogComponent,
  ],
  templateUrl: './teachers-list.component.html',
  styleUrl: './teachers-list.component.scss',
  standalone: true,
})
export class TeachersListComponent {
  @Input({ required: true }) department: Department;
  public isCreatingTeacher: boolean = false;
  public removeTeacherRequest: Teacher | null;
  public editTeacherRequest: Teacher | null;
  public registerTeacherRequest: Teacher | null;

  public handleTeacherRemoved(teacher: Teacher): void {
    this.department.teachers.splice(
      this.department.teachers.indexOf(teacher),
      1,
    );
  }

  public handleTeacherCreated(teacher: Teacher): void {
    this.department.teachers.push(teacher);
  }
}
