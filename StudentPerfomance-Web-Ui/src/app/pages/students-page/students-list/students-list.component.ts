import { Component, Input } from '@angular/core';
import { StudentGroup } from '../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { AddIconButtonComponent } from '../../../building-blocks/buttons/add-icon-button/add-icon-button.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { StudentListItemComponent } from './student-list-item/student-list-item.component';
import { NgForOf, NgIf } from '@angular/common';
import { Student } from '../../../modules/administration/submodules/students/models/student.interface';
import { CreateStudentDialogComponent } from './create-student-dialog/create-student-dialog.component';
import { EditStudentDialogComponent } from './edit-student-dialog/edit-student-dialog.component';
import { RemoveStudentDialogComponent } from './remove-student-dialog/remove-student-dialog.component';

@Component({
  selector: 'app-students-list',
  imports: [
    AddIconButtonComponent,
    GreenOutlineButtonComponent,
    StudentListItemComponent,
    NgForOf,
    NgIf,
    CreateStudentDialogComponent,
    EditStudentDialogComponent,
    RemoveStudentDialogComponent,
  ],
  templateUrl: './students-list.component.html',
  styleUrl: './students-list.component.scss',
  standalone: true,
})
export class StudentsListComponent {
  @Input({ required: true }) group: StudentGroup;
  public isCreatingStudent: boolean = false;
  public editStudentRequest: Student | null = null;
  public removeStudentRequest: Student | null = null;

  public handleStudentCreated(student: Student): void {
    this.group.students.push(student);
  }

  public handleStudentRemoved(student: Student): void {
    this.group.students.splice(this.group.students.indexOf(student), 1);
  }
}
