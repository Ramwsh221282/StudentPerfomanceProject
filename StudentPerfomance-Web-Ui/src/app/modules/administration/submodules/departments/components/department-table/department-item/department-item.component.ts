import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Department } from '../../../models/departments.interface';

@Component({
  selector: 'app-department-item',
  templateUrl: './department-item.component.html',
  styleUrl: './department-item.component.scss',
})
export class DepartmentItemComponent {
  @Input({ required: true }) isCurrentlySelected: boolean = false;
  @Input({ required: true }) department: Department;
  @Output() selected: EventEmitter<Department> = new EventEmitter();
  @Output() removeRequested: EventEmitter<Department> = new EventEmitter();

  protected isEditing: boolean = false;
}
