import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Department } from '../../../../modules/administration/submodules/departments/models/departments.interface';
import { NgClass, NgOptimizedImage } from '@angular/common';
import { EditIconButtonComponent } from '../../../../building-blocks/buttons/edit-icon-button/edit-icon-button.component';
import { YellowOutlineButtonComponent } from '../../../../building-blocks/buttons/yellow-outline-button/yellow-outline-button.component';
import { RemoveIconButtonComponent } from '../../../../building-blocks/buttons/remove-icon-button/remove-icon-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';

@Component({
  selector: 'app-department-item',
  imports: [
    NgOptimizedImage,
    EditIconButtonComponent,
    YellowOutlineButtonComponent,
    RemoveIconButtonComponent,
    RedOutlineButtonComponent,
    NgClass,
  ],
  templateUrl: './department-item.component.html',
  styleUrl: './department-item.component.scss',
  standalone: true,
})
export class DepartmentItemComponent {
  @Input({ required: true }) isSelected: boolean = false;
  @Input({ required: true }) department: Department;
  @Output() selectDepartment: EventEmitter<Department> = new EventEmitter();
  @Output() selectedForRemove: EventEmitter<Department> = new EventEmitter();
  @Output() selectedForEdit: EventEmitter<Department> = new EventEmitter();

  public selectDepartmentHandle($event: MouseEvent): void {
    $event.stopPropagation();
    this.selectDepartment.emit(this.department);
  }
}
