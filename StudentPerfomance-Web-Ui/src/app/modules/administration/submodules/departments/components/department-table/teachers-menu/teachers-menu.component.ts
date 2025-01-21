import { Component, Input } from '@angular/core';
import { Department } from '../../../models/departments.interface';
import { Teacher } from '../../../../teachers/models/teacher.interface';

@Component({
    selector: 'app-teachers-menu',
    templateUrl: './teachers-menu.component.html',
    styleUrl: './teachers-menu.component.scss',
    standalone: false
})
export class TeachersMenuComponent {
  @Input({ required: true }) department: Department;

  protected teacherToEdit: Teacher | null;
  protected isEditingTeacher: boolean = false;

  protected isCreatingTeacher: boolean = false;

  protected teacherToRemove: Teacher | null;
  protected isDeletingTeacher: boolean = false;

  protected handleTeacherCreation(teacher: Teacher): void {
    this.department.teachers.push(teacher);
    this.sortDepartmentTeachersBySurname();
  }

  protected handleTeacherDeletion(teacher: Teacher): void {
    this.department.teachers = this.department.teachers.filter(
      (t) => t.id != teacher.id,
    );
    this.sortDepartmentTeachersBySurname();
    this.teacherToRemove = null;
    this.isDeletingTeacher = false;
  }

  private sortDepartmentTeachersBySurname(): void {
    this.department.teachers.sort((a, b) =>
      a.surname > b.surname ? 1 : b.surname > a.surname ? -1 : 0,
    );
  }
}
