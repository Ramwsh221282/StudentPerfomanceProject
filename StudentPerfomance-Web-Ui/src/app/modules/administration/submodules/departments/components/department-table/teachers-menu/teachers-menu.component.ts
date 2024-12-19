import { Component, Input } from '@angular/core';
import { Department } from '../../../models/departments.interface';
import { Teacher } from '../../../../teachers/models/teacher.interface';

@Component({
  selector: 'app-teachers-menu',
  templateUrl: './teachers-menu.component.html',
  styleUrl: './teachers-menu.component.scss',
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
    const index = this.findTeacherIndexById(teacher.id);
    if (index == -1) return;
    this.department.teachers.splice(index, 1);
    this.teacherToRemove = null;
    this.isDeletingTeacher = false;
  }

  private findTeacherIndexById(id: string): number {
    let teacherIndex = -1;
    for (let i = 0; i < this.department.teachers.length; i++) {
      if (this.department.teachers[i].id == id) {
        teacherIndex = i;
        break;
      }
    }
    return teacherIndex;
  }

  private sortDepartmentTeachersBySurname(): void {
    this.department.teachers.sort((a, b) =>
      a.surname > b.surname ? 1 : b.surname > a.surname ? -1 : 0,
    );
  }
}
