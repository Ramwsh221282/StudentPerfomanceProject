import { Component, Input } from '@angular/core';
import { AddIconButtonComponent } from '../../../building-blocks/buttons/add-icon-button/add-icon-button.component';
import { DepartmentItemComponent } from '../departments-list/department-item/department-item.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { NgForOf } from '@angular/common';
import { Department } from '../../../modules/administration/submodules/departments/models/departments.interface';
import { TeacherCardComponent } from './teacher-card/teacher-card.component';

@Component({
  selector: 'app-teachers-list',
  imports: [
    AddIconButtonComponent,
    DepartmentItemComponent,
    GreenOutlineButtonComponent,
    NgForOf,
    TeacherCardComponent,
  ],
  templateUrl: './teachers-list.component.html',
  styleUrl: './teachers-list.component.scss',
  standalone: true,
})
export class TeachersListComponent {
  @Input({ required: true }) department: Department;
}
