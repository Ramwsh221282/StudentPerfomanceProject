import { Component, Input } from '@angular/core';
import { Student } from '../../../../modules/administration/submodules/students/models/student.interface';
import { NgClass, NgIf, NgOptimizedImage } from '@angular/common';
import { EditIconButtonComponent } from '../../../../building-blocks/buttons/edit-icon-button/edit-icon-button.component';
import { RemoveIconButtonComponent } from '../../../../building-blocks/buttons/remove-icon-button/remove-icon-button.component';

@Component({
  selector: 'app-student-list-item',
  imports: [
    NgOptimizedImage,
    NgIf,
    EditIconButtonComponent,
    RemoveIconButtonComponent,
    NgClass,
  ],
  templateUrl: './student-list-item.component.html',
  styleUrl: './student-list-item.component.scss',
  standalone: true,
})
export class StudentListItemComponent {
  @Input({ required: true }) student: Student;
}
