import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Department } from '../../../models/departments.interface';

@Component({
  selector: 'app-department-table-row',
  templateUrl: './department-table-row.component.html',
  styleUrl: './department-table-row.component.scss',
})
export class DepartmentTableRowComponent {
  @Input({ required: true }) department: Department;
  @Output() emitSuccess: EventEmitter<void> = new EventEmitter();
  @Output() emitFailure: EventEmitter<void> = new EventEmitter();
  @Output() emitRefresh: EventEmitter<void> = new EventEmitter();

  protected editModalVisible: boolean;
  protected removeModalVisible: boolean;
  protected teachersModalVisible: boolean;

  public constructor() {
    this.editModalVisible = false;
    this.removeModalVisible = false;
    this.teachersModalVisible = false;
  }
}
